using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.DAL.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;

        // Navigation property - one company has many contacts
        public ICollection<ContactInfo>? Contacts { get; set; }
    }
}
