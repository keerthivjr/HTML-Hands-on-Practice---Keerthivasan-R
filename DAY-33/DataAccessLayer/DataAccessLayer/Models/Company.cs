using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;

        // Navigation property - one company has many contacts
        public ICollection<ContactInfo> Contacts { get; set; } = new List<ContactInfo>();
    }
}
