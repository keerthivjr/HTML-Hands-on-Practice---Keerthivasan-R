using ContactManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.DAL.Repository
{
    public interface IContactRepository : IRepository<ContactInfo>
    {
        Task<IEnumerable<ContactInfo>> GetAllContactsWithDetailsAsync();
        Task<ContactInfo?> GetContactWithDetailsByIdAsync(int id);
    }
}
