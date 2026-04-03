using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;

        // Navigation property - one department has many contacts
        public ICollection<ContactInfo> Contacts { get; set; } = new List<ContactInfo>();
    }
}
