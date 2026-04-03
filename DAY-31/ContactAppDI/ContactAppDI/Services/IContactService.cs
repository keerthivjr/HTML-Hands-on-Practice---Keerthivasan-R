using ContactAppDI.Models;

namespace ContactAppDI.Services
{
    public interface IContactService
    {
        List<Contact> GetContacts();
        Contact GetContact(int id);
        void AddContact(Contact contact);
    }
}
