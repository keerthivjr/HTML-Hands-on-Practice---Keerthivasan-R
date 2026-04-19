using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaimAPI.Models
{
    public class ClaimDocument
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClaimId { get; set; }

        [ForeignKey("ClaimId")]
        public virtual InsuranceClaim? Claim { get; set; }

        [Required]
        public string DocumentName { get; set; } = string.Empty;

        [Required]
        public string DocumentUrl { get; set; } = string.Empty;

        public string? DocumentType { get; set; } // Police Report, Medical Report, Photo, Invoice

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public string? UploadedBy { get; set; }
    }
}