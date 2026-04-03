using Microsoft.AspNetCore.Mvc;
using ContactManagementApp.Models;
using ContactManagementApp.Services;

namespace ContactManagementApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        // Constructor Injection
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // Display all contacts
        public IActionResult ShowContacts()
        {
            var contacts = _contactService.GetAllContacts();
            return View(contacts);
        }

        // Search contact by ID
        public IActionResult GetContactById(int id)
        {
            var contact = _contactService.GetContactById(id);
            if (contact == null)
            {
                ViewBag.Message = "Contact not found!";
                return View("ContactNotFound");
            }
            return View(contact);
        }

        // GET: Show Add Contact form
        public IActionResult AddContact()
        {
            return View();
        }

        // POST: Add new contact
        [HttpPost]
        public IActionResult AddContact(ContactInfo contactInfo)
        {
            if (ModelState.IsValid)
            {
                _contactService.AddContact(contactInfo);
                return RedirectToAction("ShowContacts");
            }
            return View(contactInfo);
        }
    }
}