using DataAccessLayer.Models;
using System.Collections.Generic;

namespace DataAccessLayer.Repository
{
    public interface IContactRepository
    {
        List<ContactInfo> GetAllContacts();
        ContactInfo GetContactById(int id);
        void AddContact(ContactInfo contact);
        void UpdateContact(ContactInfo contact);
        void DeleteContact(int id);

        // Additional methods for dropdown data
        List<Company> GetAllCompanies();
        List<Department> GetAllDepartments();
    }
}