namespace ClaimsService.Models
{
    public class CreateClaimRequest
    {
        public int CustomerId { get; set; }
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public decimal ClaimAmount { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string DocumentUrl { get; set; } = string.Empty;
    }

    public class ClaimResponse
    {
        public int Id { get; set; }
        public string ClaimNumber { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public decimal ClaimAmount { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime ClaimDate { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? RejectionReason { get; set; }
    }

    public class ApproveClaimRequest
    {
        public string Status { get; set; } = string.Empty; // Approved or Rejected
        public string? RejectionReason { get; set; }
    }
}