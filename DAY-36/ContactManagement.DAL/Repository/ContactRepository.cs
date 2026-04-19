using ContactManagement.DAL.DbContext;
using ContactManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.DAL.Repository
{
    public class ContactRepository(ApplicationDbContext context) : Repository<ContactInfo>(context), IContactRepository
    {
        public async Task<IEnumerable<ContactInfo>> GetAllContactsWithDetailsAsync()
        {
            return await _context.ContactInfos
                .Include(c => c.Company)
                .Include(c => c.Department)
                .ToListAsync();
        }

        public async Task<ContactInfo?> GetContactWithDetailsByIdAsync(int id)
        {
            return await _context.ContactInfos
                .Include(c => c.Company)
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.ContactId == id);
        }
    }
}