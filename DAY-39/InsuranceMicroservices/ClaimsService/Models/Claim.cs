using System.ComponentModel.DataAnnotations;

namespace ClaimsService.Models
{
    public class Claim
    {
        [Key]
        public int Id { get; set; }
        public string ClaimNumber { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public decimal ClaimAmount { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
        public DateTime ClaimDate { get; set; } = DateTime.UtcNow;
        public DateTime? ApprovalDate { get; set; }
        public string? RejectionReason { get; set; }
        public string DocumentUrl { get; set; } = string.Empty;
    }
}