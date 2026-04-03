using ContactAppDI.Models;
using ContactAppDI.Repositories;
using ContactAppDI.Data;

namespace ContactAppDI.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repo;
        public ContactService(IContactRepository repo)
        {
            _repo = repo;
        }
        public List<Contact> GetContacts()
        {
            return _repo.GetAll();
        }
        public Contact GetContact(int id)
        {
            return _repo.GetById(id);
        }
        public void AddContact(Contact contact)
        {
            _repo.Add(contact);
        }
    }
}
