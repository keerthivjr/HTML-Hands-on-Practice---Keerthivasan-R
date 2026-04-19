namespace InsuranceClaimAPI.DTOs
{
    public class InsuranceClaimDto
    {
        public int Id { get; set; }
        public string ClaimNumber { get; set; } = string.Empty;
        public int PolicyHolderId { get; set; }
        public string PolicyHolderName { get; set; } = string.Empty;
        public string ClaimType { get; set; } = string.Empty;
        public decimal ClaimAmount { get; set; }
        public DateTime IncidentDate { get; set; }
        public string IncidentDescription { get; set; } = string.Empty;
        public string ClaimStatus { get; set; } = string.Empty;
        public DateTime SubmittedDate { get; set; }
        public decimal? ApprovedAmount { get; set; }
    }

    public class CreateClaimDto
    {
        public int PolicyHolderId { get; set; }
        public string ClaimType { get; set; } = string.Empty;
        public decimal ClaimAmount { get; set; }
        public DateTime IncidentDate { get; set; }
        public string IncidentDescription { get; set; } = string.Empty;
        public string? DocumentUrls { get; set; }
    }

    public class UpdateClaimStatusDto
    {
        public string ClaimStatus { get; set; } = string.Empty; // UnderReview, Approved, Rejected, Paid
        public decimal? ApprovedAmount { get; set; }
        public string? Remarks { get; set; }
    }
}