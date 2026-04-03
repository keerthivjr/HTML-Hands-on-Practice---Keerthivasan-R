using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; } = string.Empty;

        // Navigation Property
        public ICollection<ContactInfo> Contacts { get; set; } = new List<ContactInfo>();
    }
}