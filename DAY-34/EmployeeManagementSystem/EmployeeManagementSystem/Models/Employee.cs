using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public string? Department { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(1000, 500000, ErrorMessage = "Salary must be between 1000 and 500000")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Job Status is required")]
        public string? JobStatus { get; set; } // Active, Inactive, On Leave

        [Required(ErrorMessage = "Hire Date is required")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
    }
}