using DataAccessLayer.DbContext;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<ContactInfo> GetAllContacts()
        {
            return _context.Contacts
                .Include(c => c.Company)
                .Include(c => c.Department)
                .ToList();
        }

        public ContactInfo GetContactById(int id)
        {
            return _context.Contacts
                .Include(c => c.Company)
                .Include(c => c.Department)
                .FirstOrDefault(c => c.ContactId == id);
        }

        public void AddContact(ContactInfo contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
        }

        public void UpdateContact(ContactInfo contact)
        {
            var existingContact = _context.Contacts.Find(contact.ContactId);
            if (existingContact != null)
            {
                _context.Entry(existingContact).CurrentValues.SetValues(contact);
                _context.SaveChanges();
            }
        }

        public void DeleteContact(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                _context.SaveChanges();
            }
        }

        public List<Company> GetAllCompanies()
        {
            return _context.Companies.ToList();
        }

        public List<Department> GetAllDepartments()
        {
            return _context.Departments.ToList();
        }
    }
}