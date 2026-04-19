using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PolicyService.Data;
using PolicyService.Models;

namespace PolicyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  // ✅ This requires authentication for ALL endpoints
    public class PolicyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PolicyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/policy
        [HttpGet]
        public async Task<IActionResult> GetAllPolicies()
        {
            var policies = await _context.Policies
                .Select(p => new PolicyDto
                {
                    Id = p.Id,
                    PolicyNumber = p.PolicyNumber,
                    PolicyName = p.PolicyName,
                    PolicyType = p.PolicyType,
                    CoverageAmount = p.CoverageAmount,
                    PremiumAmount = p.PremiumAmount,
                    DurationYears = p.DurationYears,
                    Status = p.Status
                })
                .ToListAsync();

            return Ok(policies);
        }

        // GET: api/policy/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicyById(int id)
        {
            var policy = await _context.Policies.FindAsync(id);
            if (policy == null)
                return NotFound(new { message = "Policy not found" });

            return Ok(new PolicyDto
            {
                Id = policy.Id,
                PolicyNumber = policy.PolicyNumber,
                PolicyName = policy.PolicyName,
                PolicyType = policy.PolicyType,
                CoverageAmount = policy.CoverageAmount,
                PremiumAmount = policy.PremiumAmount,
                DurationYears = policy.DurationYears,
                Status = policy.Status
            });
        }

        // POST: api/policy
        [HttpPost]
        [Authorize(Roles = "Admin")]  // ✅ Only Admin can create policies
        public async Task<IActionResult> CreatePolicy(CreatePolicyRequest request)
        {
            var policy = new Policy
            {
                PolicyNumber = $"POL{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                PolicyName = request.PolicyName,
                PolicyType = request.PolicyType,
                CoverageAmount = request.CoverageAmount,
                PremiumAmount = request.PremiumAmount,
                DurationYears = request.DurationYears,
                Status = "Active"
            };

            _context.Policies.Add(policy);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPolicyById), new { id = policy.Id }, policy);
        }

        // POST: api/policy/assign
        [HttpPost("assign")]
        [Authorize(Roles = "Admin,Agent")]  // ✅ Admin OR Agent can assign policies
        public async Task<IActionResult> AssignPolicyToCustomer(AssignPolicyRequest request)
        {
            var policy = await _context.Policies.FindAsync(request.PolicyId);
            if (policy == null)
                return NotFound(new { message = "Policy not found" });

            // Check if customer already has this policy
            var existing = await _context.CustomerPolicies
                .FirstOrDefaultAsync(cp => cp.CustomerId == request.CustomerId && cp.PolicyId == request.PolicyId);

            if (existing != null)
                return BadRequest(new { message = "Customer already has this policy" });

            var customerPolicy = new CustomerPolicy
            {
                CustomerId = request.CustomerId,
                PolicyId = request.PolicyId,
                PolicyNumber = policy.PolicyNumber,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddYears(policy.DurationYears),
                Status = "Active"
            };

            _context.CustomerPolicies.Add(customerPolicy);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Policy assigned successfully", assignment = customerPolicy });
        }

        // GET: api/policy/customer/{customerId}
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCustomerPolicies(int customerId)
        {
            // Get the current user's role and ID from the token
            var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            var userName = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            // In a real implementation, you'd get the customer ID from a mapping table
            // For now, we'll allow all authenticated users to view customer policies
            // But in production, you should restrict customers to only see their own policies

            var policies = await _context.CustomerPolicies
                .Include(cp => cp.Policy)
                .Where(cp => cp.CustomerId == customerId)
                .Select(cp => new CustomerPolicyDto
                {
                    Id = cp.Id,
                    CustomerId = cp.CustomerId,
                    PolicyId = cp.PolicyId,
                    PolicyNumber = cp.PolicyNumber,
                    PolicyName = cp.Policy != null ? cp.Policy.PolicyName : string.Empty,
                    PolicyType = cp.Policy != null ? cp.Policy.PolicyType : string.Empty,
                    CoverageAmount = cp.Policy != null ? cp.Policy.CoverageAmount : 0,
                    PremiumAmount = cp.Policy != null ? cp.Policy.PremiumAmount : 0,
                    StartDate = cp.StartDate,
                    EndDate = cp.EndDate,
                    Status = cp.Status
                })
                .ToListAsync();

            return Ok(policies);
        }

        // PUT: api/policy/{id}/cancel
        [HttpPut("{id}/cancel")]
        [Authorize(Roles = "Admin,Agent")]
        public async Task<IActionResult> CancelPolicy(int id)
        {
            var customerPolicy = await _context.CustomerPolicies.FindAsync(id);
            if (customerPolicy == null)
                return NotFound(new { message = "Policy assignment not found" });

            customerPolicy.Status = "Cancelled";
            await _context.SaveChangesAsync();

            return Ok(new { message = "Policy cancelled successfully" });
        }
    }
}