using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; }

        // Navigation Property
        public virtual ICollection<ContactInfo> Contacts { get; set; }

        public Company()
        {
            Contacts = new List<ContactInfo>();
        }
    }
}