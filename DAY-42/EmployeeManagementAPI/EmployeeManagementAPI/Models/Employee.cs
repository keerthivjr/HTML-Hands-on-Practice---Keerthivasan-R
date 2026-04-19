using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementAPI.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

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
        public string Position { get; set; } = string.Empty;

        [Range(0, 1000000)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        public DateTime HireDate { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}