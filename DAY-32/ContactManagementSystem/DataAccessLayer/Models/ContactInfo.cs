using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class ContactInfo
    {
        [Key]
        public int ContactId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        // ✅ ADD THIS PROPERTY HERE
        // The [NotMapped] attribute tells Entity Framework NOT to create a column in SQL.
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        [Required]
        [EmailAddress]
        public string EmailId { get; set; } = null!;

        [Required]
        public long MobileNo { get; set; }

        [StringLength(100)]
        public string Designation { get; set; } = null!;

        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company? Company { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }
    }
}