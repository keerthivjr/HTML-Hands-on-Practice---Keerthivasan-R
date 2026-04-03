using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccessLayer.Models
{
   
public class ContactInfo
    {
        [Key]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [Range(1000000000, 9999999999, ErrorMessage = "Enter valid 10-digit mobile number")]
        public long MobileNo { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Select Company")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Select Department")]
        public int DepartmentId { get; set; }

        public Company? Company { get; set; }
        public Department? Department { get; set; }
    }
}

