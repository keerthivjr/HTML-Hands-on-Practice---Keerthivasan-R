using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string DepartmentName { get; set; }

        // Navigation Property
        public virtual ICollection<ContactInfo> Contacts { get; set; }

        public Department()
        {
            Contacts = new List<ContactInfo>();
        }
    }
}