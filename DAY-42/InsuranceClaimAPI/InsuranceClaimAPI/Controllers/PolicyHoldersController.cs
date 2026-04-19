using InsuranceClaimAPI.Data;
using InsuranceClaimAPI.DTOs;
using InsuranceClaimAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceClaimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PolicyHoldersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PolicyHoldersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/policyholders
        [HttpGet]
        [Authorize(Roles = "Admin,ClaimsProcessor")]
        public async Task<ActionResult<IEnumerable<PolicyHolderDto>>> GetPolicyHolders()
        {
            var policyHolders = await _context.PolicyHolders
                .Select(p => new PolicyHolderDto
                {
                    Id = p.Id,
                    PolicyNumber = p.PolicyNumber,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber,
                    Address = p.Address,
                    PolicyType = p.PolicyType,
                    CoverageAmount = p.CoverageAmount,
                    PolicyStartDate = p.PolicyStartDate,
                    PolicyEndDate = p.PolicyEndDate,
                    IsActive = p.IsActive
                })
                .ToListAsync();

            return Ok(policyHolders);
        }

        // GET: api/policyholders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PolicyHolderDto>> GetPolicyHolder(int id)
        {
            var policyHolder = await _context.PolicyHolders.FindAsync(id);

            if (policyHolder == null)
                return NotFound(new { message = $"Policy holder with ID {id} not found" });

            var policyHolderDto = new PolicyHolderDto
            {
                Id = policyHolder.Id,
                PolicyNumber = policyHolder.PolicyNumber,
                FirstName = policyHolder.FirstName,
                LastName = policyHolder.LastName,
                Email = policyHolder.Email,
                PhoneNumber = policyHolder.PhoneNumber,
                Address = policyHolder.Address,
                PolicyType = policyHolder.PolicyType,
                CoverageAmount = policyHolder.CoverageAmount,
                PolicyStartDate = policyHolder.PolicyStartDate,
                PolicyEndDate = policyHolder.PolicyEndDate,
                IsActive = policyHolder.IsActive
            };

            return Ok(policyHolderDto);
        }

        // POST: api/policyholders
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PolicyHolderDto>> CreatePolicyHolder([FromBody] CreatePolicyHolderDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.PolicyHolders.AnyAsync(p => p.PolicyNumber == createDto.PolicyNumber))
                return BadRequest(new { message = "Policy number already exists" });

            if (await _context.PolicyHolders.AnyAsync(p => p.Email == createDto.Email))
                return BadRequest(new { message = "Email already exists" });

            var policyHolder = new PolicyHolder
            {
                PolicyNumber = createDto.PolicyNumber,
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                Email = createDto.Email,
                PhoneNumber = createDto.PhoneNumber,
                Address = createDto.Address,
                PolicyType = createDto.PolicyType,
                CoverageAmount = createDto.CoverageAmount,
                PolicyStartDate = createDto.PolicyStartDate,
                PolicyEndDate = createDto.PolicyEndDate,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.PolicyHolders.Add(policyHolder);
            await _context.SaveChangesAsync();

            var policyHolderDto = new PolicyHolderDto
            {
                Id = policyHolder.Id,
                PolicyNumber = policyHolder.PolicyNumber,
                FirstName = policyHolder.FirstName,
                LastName = policyHolder.LastName,
                Email = policyHolder.Email,
                PhoneNumber = policyHolder.PhoneNumber,
                Address = policyHolder.Address,
                PolicyType = policyHolder.PolicyType,
                CoverageAmount = policyHolder.CoverageAmount,
                PolicyStartDate = policyHolder.PolicyStartDate,
                PolicyEndDate = policyHolder.PolicyEndDate,
                IsActive = policyHolder.IsActive
            };

            return CreatedAtAction(nameof(GetPolicyHolder), new { id = policyHolder.Id }, policyHolderDto);
        }

        // PUT: api/policyholders/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePolicyHolder(int id, [FromBody] UpdatePolicyHolderDto updateDto)
        {
            var policyHolder = await _context.PolicyHolders.FindAsync(id);

            if (policyHolder == null)
                return NotFound(new { message = $"Policy holder with ID {id} not found" });

            if (policyHolder.Email != updateDto.Email &&
                await _context.PolicyHolders.AnyAsync(p => p.Email == updateDto.Email))
                return BadRequest(new { message = "Email already exists" });

            policyHolder.FirstName = updateDto.FirstName;
            policyHolder.LastName = updateDto.LastName;
            policyHolder.Email = updateDto.Email;
            policyHolder.PhoneNumber = updateDto.PhoneNumber;
            policyHolder.Address = updateDto.Address;
            policyHolder.IsActive = updateDto.IsActive;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Policy holder updated successfully" });
        }

        // DELETE: api/policyholders/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePolicyHolder(int id)
        {
            var policyHolder = await _context.PolicyHolders
                .Include(p => p.Claims)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (policyHolder == null)
                return NotFound(new { message = $"Policy holder with ID {id} not found" });

            if (policyHolder.Claims.Any())
                return BadRequest(new { message = "Cannot delete policy holder with existing claims" });

            _context.PolicyHolders.Remove(policyHolder);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Policy holder deleted successfully" });
        }
    }
}