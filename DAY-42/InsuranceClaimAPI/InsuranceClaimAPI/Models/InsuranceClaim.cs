using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaimAPI.Models
{
    public class InsuranceClaim
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ClaimNumber { get; set; } = string.Empty;

        [Required]
        public int PolicyHolderId { get; set; }

        [ForeignKey("PolicyHolderId")]
        public virtual PolicyHolder? PolicyHolder { get; set; }

        [Required]
        public string ClaimType { get; set; } = string.Empty; // Accident, Theft, Natural Disaster, Health

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ClaimAmount { get; set; }

        [Required]
        public DateTime IncidentDate { get; set; }

        [Required]
        public string IncidentDescription { get; set; } = string.Empty;

        public string? DocumentUrls { get; set; } // Comma-separated URLs

        [Required]
        public string ClaimStatus { get; set; } = "Submitted"; // Submitted, UnderReview, Approved, Rejected, Paid

        public string? Remarks { get; set; }

        public DateTime? ApprovalDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ApprovedAmount { get; set; }

        public DateTime SubmittedDate { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdatedDate { get; set; }

        public string? ProcessedBy { get; set; }
    }
}