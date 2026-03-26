using System.ComponentModel.DataAnnotations;

namespace ContactApp.Models
{
    public class ContactInfo
    {
        [Required(ErrorMessage = "Contact ID is required")]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "First Name cannot be empty")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        public string CompanyName { get; set; } // Optional field

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        public long MobileNo { get; set; }

        public string Designation { get; set; }
    }
}
