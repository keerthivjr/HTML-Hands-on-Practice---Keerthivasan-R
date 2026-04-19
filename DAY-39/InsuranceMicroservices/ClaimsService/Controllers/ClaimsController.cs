using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ClaimsService.Data;
using ClaimsService.Models;
using System.Security.Claims;

namespace ClaimsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClaimsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClaimsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/claims
        [HttpGet]
        [Authorize(Roles = "Admin,Agent")]
        public async Task<IActionResult> GetAllClaims()
        {
            var allClaims = await _context.Claims
                .Select(c => new ClaimResponse
                {
                    Id = c.Id,
                    ClaimNumber = c.ClaimNumber,
                    CustomerId = c.CustomerId,
                    PolicyId = c.PolicyId,
                    PolicyNumber = c.PolicyNumber,
                    ClaimAmount = c.ClaimAmount,
                    Reason = c.Reason,
                    Status = c.Status,
                    ClaimDate = c.ClaimDate,
                    ApprovalDate = c.ApprovalDate,
                    RejectionReason = c.RejectionReason
                })
                .ToListAsync();

            return Ok(allClaims);
        }

        // GET: api/claims/customer/{customerId}
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetCustomerClaims(int customerId)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;

            var customerClaims = await _context.Claims
                .Where(c => c.CustomerId == customerId)
                .Select(c => new ClaimResponse
                {
                    Id = c.Id,
                    ClaimNumber = c.ClaimNumber,
                    CustomerId = c.CustomerId,
                    PolicyId = c.PolicyId,
                    PolicyNumber = c.PolicyNumber,
                    ClaimAmount = c.ClaimAmount,
                    Reason = c.Reason,
                    Status = c.Status,
                    ClaimDate = c.ClaimDate,
                    ApprovalDate = c.ApprovalDate,
                    RejectionReason = c.RejectionReason
                })
                .ToListAsync();

            return Ok(customerClaims);
        }

        // GET: api/claims/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClaimById(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null)
                return NotFound(new { message = "Claim not found" });

            return Ok(new ClaimResponse
            {
                Id = claim.Id,
                ClaimNumber = claim.ClaimNumber,
                CustomerId = claim.CustomerId,
                PolicyId = claim.PolicyId,
                PolicyNumber = claim.PolicyNumber,
                ClaimAmount = claim.ClaimAmount,
                Reason = claim.Reason,
                Status = claim.Status,
                ClaimDate = claim.ClaimDate,
                ApprovalDate = claim.ApprovalDate,
                RejectionReason = claim.RejectionReason
            });
        }

        // POST: api/claims
        [HttpPost]
        public async Task<IActionResult> SubmitClaim(CreateClaimRequest request)
        {
            // ✅ FIX: Use fully qualified name ClaimsService.Models.Claim
            var newClaim = new ClaimsService.Models.Claim
            {
                ClaimNumber = $"CLM{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                CustomerId = request.CustomerId,
                PolicyId = request.PolicyId,
                PolicyNumber = request.PolicyNumber,
                ClaimAmount = request.ClaimAmount,
                Reason = request.Reason,
                Status = "Pending",
                DocumentUrl = request.DocumentUrl
            };

            _context.Claims.Add(newClaim);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Claim submitted successfully", claimNumber = newClaim.ClaimNumber });
        }

        // PUT: api/claims/{id}/approve
        [HttpPut("{id}/approve")]
        [Authorize(Roles = "Admin,Agent")]
        public async Task<IActionResult> ApproveClaim(int id, ApproveClaimRequest request)
        {
            var existingClaim = await _context.Claims.FindAsync(id);
            if (existingClaim == null)
                return NotFound(new { message = "Claim not found" });

            existingClaim.Status = request.Status;
            existingClaim.ApprovalDate = DateTime.UtcNow;

            if (request.Status == "Rejected")
                existingClaim.RejectionReason = request.RejectionReason;

            await _context.SaveChangesAsync();

            return Ok(new { message = $"Claim {request.Status.ToLower()} successfully" });
        }

        // GET: api/claims/status/pending
        [HttpGet("status/pending")]
        [Authorize(Roles = "Admin,Agent")]
        public async Task<IActionResult> GetPendingClaims()
        {
            var pendingClaims = await _context.Claims
                .Where(c => c.Status == "Pending")
                .Select(c => new ClaimResponse
                {
                    Id = c.Id,
                    ClaimNumber = c.ClaimNumber,
                    CustomerId = c.CustomerId,
                    PolicyId = c.PolicyId,
                    PolicyNumber = c.PolicyNumber,
                    ClaimAmount = c.ClaimAmount,
                    Reason = c.Reason,
                    Status = c.Status,
                    ClaimDate = c.ClaimDate
                })
                .ToListAsync();

            return Ok(pendingClaims);
        }
    }
}