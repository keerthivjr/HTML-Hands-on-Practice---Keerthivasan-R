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
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string EmailId { get; set; } = string.Empty;

        [Required]
        [Phone]
        [Display(Name = "Mobile Number")]
        public long MobileNo { get; set; }

        [StringLength(100)]
        [Display(Name = "Designation")]
        public string Designation { get; set; } = string.Empty;

        // Foreign Keys
        [Required]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        // Navigation Properties
        [ForeignKey("CompanyId")]
        public virtual Company? Company { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }

        // Computed Property
        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";
    }
}