using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceClaimAPI.Models
{
    public class PolicyHolder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string PolicyNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string PolicyType { get; set; } = string.Empty; // Health, Auto, Home, Life

        [Column(TypeName = "decimal(18,2)")]
        public decimal CoverageAmount { get; set; }

        public DateTime PolicyStartDate { get; set; }

        public DateTime PolicyEndDate { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ICollection<InsuranceClaim> Claims { get; set; } = new List<InsuranceClaim>();
    }
}