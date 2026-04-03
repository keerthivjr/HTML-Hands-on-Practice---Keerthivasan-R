using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ContactInfo
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public long MobileNo { get; set; }
        public string Designation { get; set; } = string.Empty;

        // Foreign Keys
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }

        // Navigation properties
        public Company Company { get; set; } = null!;
        public Department Department { get; set; } = null!;
    }
}
