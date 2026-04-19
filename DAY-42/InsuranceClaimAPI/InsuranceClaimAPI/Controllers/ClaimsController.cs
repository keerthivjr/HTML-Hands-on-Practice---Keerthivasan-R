using InsuranceClaimAPI.Data;
using InsuranceClaimAPI.DTOs;
using InsuranceClaimAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InsuranceClaimAPI.Controllers
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
        [Authorize(Roles = "Admin,ClaimsProcessor")]
        public async Task<ActionResult<IEnumerable<InsuranceClaimDto>>> GetAllClaims()
        {
            var claims = await _context.InsuranceClaims
                .Include(c => c.PolicyHolder)
                .Select(c => new InsuranceClaimDto
                {
                    Id = c.Id,
                    ClaimNumber = c.ClaimNumber,
                    PolicyHolderId = c.PolicyHolderId,
                    PolicyHolderName = $"{c.PolicyHolder!.FirstName} {c.PolicyHolder.LastName}",
                    ClaimType = c.ClaimType,
                    ClaimAmount = c.ClaimAmount,
                    IncidentDate = c.IncidentDate,
                    IncidentDescription = c.IncidentDescription,
                    ClaimStatus = c.ClaimStatus,
                    SubmittedDate = c.SubmittedDate,
                    ApprovedAmount = c.ApprovedAmount
                })
                .ToListAsync();

            return Ok(claims);
        }

        // GET: api/claims/myclaims
        [HttpGet("myclaims")]
        public async Task<ActionResult<IEnumerable<InsuranceClaimDto>>> GetMyClaims()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var user = await _context.Users.FindAsync(userId);

            // For PolicyHolder role, get claims linked to their policy holder record
            // For simplicity, we'll return all claims (you can enhance this logic)
            var claims = await _context.InsuranceClaims
                .Include(c => c.PolicyHolder)
                .Select(c => new InsuranceClaimDto
                {
                    Id = c.Id,
                    ClaimNumber = c.ClaimNumber,
                    PolicyHolderId = c.PolicyHolderId,
                    PolicyHolderName = $"{c.PolicyHolder!.FirstName} {c.PolicyHolder.LastName}",
                    ClaimType = c.ClaimType,
                    ClaimAmount = c.ClaimAmount,
                    IncidentDate = c.IncidentDate,
                    IncidentDescription = c.IncidentDescription,
                    ClaimStatus = c.ClaimStatus,
                    SubmittedDate = c.SubmittedDate,
                    ApprovedAmount = c.ApprovedAmount
                })
                .ToListAsync();

            return Ok(claims);
        }

        // GET: api/claims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InsuranceClaimDto>> GetClaim(int id)
        {
            var claim = await _context.InsuranceClaims
                .Include(c => c.PolicyHolder)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (claim == null)
                return NotFound(new { message = $"Claim with ID {id} not found" });

            var claimDto = new InsuranceClaimDto
            {
                Id = claim.Id,
                ClaimNumber = claim.ClaimNumber,
                PolicyHolderId = claim.PolicyHolderId,
                PolicyHolderName = $"{claim.PolicyHolder!.FirstName} {claim.PolicyHolder.LastName}",
                ClaimType = claim.ClaimType,
                ClaimAmount = claim.ClaimAmount,
                IncidentDate = claim.IncidentDate,
                IncidentDescription = claim.IncidentDescription,
                ClaimStatus = claim.ClaimStatus,
                SubmittedDate = claim.SubmittedDate,
                ApprovedAmount = claim.ApprovedAmount
            };

            return Ok(claimDto);
        }

        // POST: api/claims
        [HttpPost]
        public async Task<ActionResult<InsuranceClaimDto>> CreateClaim([FromBody] CreateClaimDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var policyHolder = await _context.PolicyHolders.FindAsync(createDto.PolicyHolderId);
            if (policyHolder == null)
                return BadRequest(new { message = "Policy holder not found" });

            // Generate unique claim number
            var claimNumber = $"CLM-{DateTime.Now:yyyyMMdd}-{new Random().Next(1000, 9999)}";

            var claim = new InsuranceClaim
            {
                ClaimNumber = claimNumber,
                PolicyHolderId = createDto.PolicyHolderId,
                ClaimType = createDto.ClaimType,
                ClaimAmount = createDto.ClaimAmount,
                IncidentDate = createDto.IncidentDate,
                IncidentDescription = createDto.IncidentDescription,
                DocumentUrls = createDto.DocumentUrls,
                ClaimStatus = "Submitted",
                SubmittedDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow
            };

            _context.InsuranceClaims.Add(claim);
            await _context.SaveChangesAsync();

            var claimDto = new InsuranceClaimDto
            {
                Id = claim.Id,
                ClaimNumber = claim.ClaimNumber,
                PolicyHolderId = claim.PolicyHolderId,
                PolicyHolderName = $"{policyHolder.FirstName} {policyHolder.LastName}",
                ClaimType = claim.ClaimType,
                ClaimAmount = claim.ClaimAmount,
                IncidentDate = claim.IncidentDate,
                IncidentDescription = claim.IncidentDescription,
                ClaimStatus = claim.ClaimStatus,
                SubmittedDate = claim.SubmittedDate,
                ApprovedAmount = claim.ApprovedAmount
            };

            return CreatedAtAction(nameof(GetClaim), new { id = claim.Id }, claimDto);
        }

        // PUT: api/claims/5/status
        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin,ClaimsProcessor")]
        public async Task<IActionResult> UpdateClaimStatus(int id, [FromBody] UpdateClaimStatusDto updateDto)
        {
            var claim = await _context.InsuranceClaims.FindAsync(id);

            if (claim == null)
                return NotFound(new { message = $"Claim with ID {id} not found" });

            var validStatuses = new[] { "UnderReview", "Approved", "Rejected", "Paid" };
            if (!validStatuses.Contains(updateDto.ClaimStatus))
                return BadRequest(new { message = "Invalid claim status" });

            claim.ClaimStatus = updateDto.ClaimStatus;
            claim.ApprovedAmount = updateDto.ApprovedAmount;
            claim.Remarks = updateDto.Remarks;
            claim.LastUpdatedDate = DateTime.UtcNow;
            claim.ProcessedBy = User.FindFirst(ClaimTypes.Name)?.Value;

            if (updateDto.ClaimStatus == "Approved" || updateDto.ClaimStatus == "Rejected")
            {
                claim.ApprovalDate = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = $"Claim status updated to {updateDto.ClaimStatus}" });
        }

        // DELETE: api/claims/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteClaim(int id)
        {
            var claim = await _context.InsuranceClaims.FindAsync(id);

            if (claim == null)
                return NotFound(new { message = $"Claim with ID {id} not found" });

            if (claim.ClaimStatus != "Submitted")
                return BadRequest(new { message = "Cannot delete claim that is already under review or processed" });

            _context.InsuranceClaims.Remove(claim);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Claim deleted successfully" });
        }

        // GET: api/claims/statistics
        [HttpGet("statistics")]
        [Authorize(Roles = "Admin,ClaimsProcessor")]
        public async Task<IActionResult> GetClaimStatistics()
        {
            var totalClaims = await _context.InsuranceClaims.CountAsync();
            var approvedClaims = await _context.InsuranceClaims.CountAsync(c => c.ClaimStatus == "Approved");
            var rejectedClaims = await _context.InsuranceClaims.CountAsync(c => c.ClaimStatus == "Rejected");
            var pendingClaims = await _context.InsuranceClaims.CountAsync(c => c.ClaimStatus == "Submitted" || c.ClaimStatus == "UnderReview");
            var totalAmount = await _context.InsuranceClaims.SumAsync(c => c.ClaimAmount);
            var approvedAmount = await _context.InsuranceClaims.SumAsync(c => c.ApprovedAmount ?? 0);

            return Ok(new
            {
                TotalClaims = totalClaims,
                ApprovedClaims = approvedClaims,
                RejectedClaims = rejectedClaims,
                PendingClaims = pendingClaims,
                TotalClaimAmount = totalAmount,
                TotalApprovedAmount = approvedAmount
            });
        }
    }
}