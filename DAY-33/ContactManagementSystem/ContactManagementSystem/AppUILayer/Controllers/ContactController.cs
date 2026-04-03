using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppUILayer.Controllers
{

    
    public class ContactController : Controller
    {
        private readonly IContactRepository _repo;

        public ContactController(IContactRepository repo)
        {
            _repo = repo;
        }

        public IActionResult ShowContacts()
        {
            var contacts = _repo.GetAllContacts();
            return View(contacts);
        }

        public IActionResult AddContact()
        {
            LoadDropdowns();
            return View();
        }
        [HttpPost]
        public IActionResult AddContact(ContactInfo contact)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdowns();   
                return View(contact);
            }

            _repo.AddContact(contact);
            return RedirectToAction("ShowContacts");
        }

        private void LoadDropdowns()
        {
            ViewBag.Companies = new SelectList(_repo.GetCompanies(), "CompanyId", "CompanyName");
            ViewBag.Departments = new SelectList(_repo.GetDepartments(), "DepartmentId", "DepartmentName");
        }

        public IActionResult EditContact(int id)
        {
            var contact = _repo.GetContactById(id);
            LoadDropdowns();
            return View(contact);
        }

        [HttpPost]
        public IActionResult EditContact(ContactInfo contact)
        {
            _repo.UpdateContact(contact);
            return RedirectToAction("ShowContacts");
        }

        public IActionResult DeleteContact(int id)
        {
            _repo.DeleteContact(id);
            return RedirectToAction("ShowContacts");
        }
    }


}


