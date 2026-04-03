using ContactAppDI.Data;
using ContactAppDI.Models;

namespace ContactAppDI.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;
        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Contact> GetAll()
        { 
            return _context.Contacts.ToList();
        }
        public Contact GetById(int id)
        {
            return _context.Contacts.Find(id);
        }
        public void Add(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
        }

    }
}
