using System.ComponentModel.DataAnnotations;

namespace PolicyService.Models
{
    public class Policy
    {
        [Key]
        public int Id { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public string PolicyName { get; set; } = string.Empty;
        public string PolicyType { get; set; } = string.Empty; // Life, Health, Auto, Home
        public decimal CoverageAmount { get; set; }
        public decimal PremiumAmount { get; set; }
        public int DurationYears { get; set; }
        public string Status { get; set; } = "Active"; // Active, Inactive, Expired
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class CustomerPolicy
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int PolicyId { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "Active"; // Active, Expired, Cancelled
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Policy? Policy { get; set; }
    }
}