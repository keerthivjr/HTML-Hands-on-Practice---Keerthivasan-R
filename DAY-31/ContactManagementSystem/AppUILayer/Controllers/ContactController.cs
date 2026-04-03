using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Data;
using System.Linq;

namespace AppUILayer.Controllers
{
    [Route("[controller]")]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly AppDbContext _context;

        public ContactController(IContactRepository contactRepository, AppDbContext context)
        {
            _contactRepository = contactRepository;
            _context = context;
        }

        // GET: Contact/ShowContacts
        [Route("ShowContacts")]
        [HttpGet]
        public IActionResult ShowContacts()
        {
            var contacts = _contactRepository.GetAllContacts();
            return View(contacts);
        }

        // GET: Contact/GetContactById/5
        [Route("GetContactById/{id}")]
        [HttpGet]
        public IActionResult GetContactById(int id)
        {
            var contact = _contactRepository.GetContactById(id);
            if (contact == null)
            {
                TempData["ErrorMessage"] = "Contact not found!";
                return RedirectToAction("ShowContacts");
            }
            return View(contact);
        }

        // GET: Contact/AddContact
        [Route("AddContact")]
        [HttpGet]
        public IActionResult AddContact()
        {
            LoadDropDownLists();
            return View();
        }

        // POST: Contact/AddContact
        [Route("AddContact")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddContact(ContactInfo contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _contactRepository.AddContact(contact);
                    TempData["SuccessMessage"] = "Contact added successfully!";
                    return RedirectToAction("ShowContacts");
                }
                catch
                {
                    ModelState.AddModelError("", "Error saving contact. Please try again.");
                }
            }
            LoadDropDownLists();
            return View(contact);
        }

        // GET: Contact/EditContact/5
        [Route("EditContact/{id}")]
        [HttpGet]
        public IActionResult EditContact(int id)
        {
            var contact = _contactRepository.GetContactById(id);
            if (contact == null)
            {
                TempData["ErrorMessage"] = "Contact not found!";
                return RedirectToAction("ShowContacts");
            }
            LoadDropDownLists(contact.CompanyId, contact.DepartmentId);
            return View(contact);
        }

        // POST: Contact/EditContact/5
        [Route("EditContact/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditContact(int id, ContactInfo contact)
        {
            if (id != contact.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contactRepository.UpdateContact(contact);
                    TempData["SuccessMessage"] = "Contact updated successfully!";
                    return RedirectToAction("ShowContacts");
                }
                catch
                {
                    ModelState.AddModelError("", "Error updating contact. Please try again.");
                }
            }
            LoadDropDownLists(contact.CompanyId, contact.DepartmentId);
            return View(contact);
        }

        // GET: Contact/DeleteContact/5
        [Route("DeleteContact/{id}")]
        [HttpGet]
        public IActionResult DeleteContact(int id)
        {
            var contact = _contactRepository.GetContactById(id);
            if (contact == null)
            {
                TempData["ErrorMessage"] = "Contact not found!";
                return RedirectToAction("ShowContacts");
            }
            return View(contact);
        }

        // POST: Contact/DeleteContact/5
        [Route("DeleteContact/{id}")]
        [HttpPost, ActionName("DeleteContact")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteContactConfirmed(int id)
        {
            try
            {
                _contactRepository.DeleteContact(id);
                TempData["SuccessMessage"] = "Contact deleted successfully!";
            }
            catch
            {
                TempData["ErrorMessage"] = "Error deleting contact. Please try again.";
            }
            return RedirectToAction("ShowContacts");
        }

        // Helper method to load dropdown lists
        private void LoadDropDownLists(int? selectedCompanyId = null, int? selectedDepartmentId = null)
        {
            // Load companies
            var companies = _context.Companies.ToList();
            ViewBag.Companies = companies;

            // Load departments
            var departments = _context.Departments.ToList();
            ViewBag.Departments = departments;

            // Set selected values if provided
            if (selectedCompanyId.HasValue)
                ViewBag.SelectedCompany = selectedCompanyId.Value;
            if (selectedDepartmentId.HasValue)
                ViewBag.SelectedDepartment = selectedDepartmentId.Value;
        }
    }
}