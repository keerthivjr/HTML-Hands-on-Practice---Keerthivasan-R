using ContactManagementApp.Models;

namespace ContactManagementApp.Services
{
    public class ContactService : IContactService
    {
        private static List<ContactInfo> contacts = new List<ContactInfo>();
        private static int nextId = 1;

        public List<ContactInfo> GetAllContacts()
        {
            return contacts;
        }

        public ContactInfo? GetContactById(int id)
        {
            return contacts.FirstOrDefault(c => c.ContactId == id);
        }

        public void AddContact(ContactInfo contact)
        {
            contact.ContactId = nextId++;
            contacts.Add(contact);
        }
    }
}
