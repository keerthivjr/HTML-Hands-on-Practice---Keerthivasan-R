using ContactAppDI.Models;

namespace ContactAppDI.Repositories
{
    public interface IContactRepository
    {
        List<Contact> GetAll();
        Contact GetById(int id);
        void Add(Contact contact);
    }
}
