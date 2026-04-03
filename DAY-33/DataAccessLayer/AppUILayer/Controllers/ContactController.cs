using DataAccessLayer.DbContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppUILayer.Controllers
{
    [Route("contact")]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly AppDbContext _context;

        public ContactController(IContactRepository contactRepository, AppDbContext context)
        {
            _contactRepository = contactRepository;
            _context = context;
        }

        // GET: Show all contacts
        [Route("show")]
        [Route("")]
        public IActionResult ShowContacts()
        {
            var contacts = _contactRepository.GetAllContacts();
            return View(contacts);
        }

        // GET: Contact details by id
        [Route("details/{id}")]
        public IActionResult GetContactById(int id)
        {
            var contact = _contactRepository.GetContactById(id);
            if (contact == null)
                return NotFound();
            return View(contact);
        }

        // GET: Add Contact Form
        [Route("add")]
        public IActionResult AddContact()
        {
            LoadDropDowns();
            return View();
        }

        // POST: Save Contact
        [HttpPost]
        [Route("add")]
        [ValidateAntiForgeryToken]
        public IActionResult AddContact(ContactInfo contact)
        {
            if (ModelState.IsValid)
            {
                _contactRepository.AddContact(contact);
                return RedirectToAction("ShowContacts");
            }
            LoadDropDowns();
            return View(contact);
        }

        // GET: Edit Contact Form
        [Route("edit/{id}")]
        public IActionResult EditContact(int id)
        {
            var contact = _contactRepository.GetContactById(id);
            if (contact == null)
                return NotFound();
            LoadDropDowns();
            return View(contact);
        }

        // POST: Update Contact
        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult EditContact(int id, ContactInfo contact)
        {
            if (id != contact.ContactId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _contactRepository.UpdateContact(contact);
                return RedirectToAction("ShowContacts");
            }
            LoadDropDowns();
            return View(contact);
        }

        // POST: Delete Contact
        [HttpPost]
        [Route("delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteContact(int id)
        {
            _contactRepository.DeleteContact(id);
            return RedirectToAction("ShowContacts");
        }

        // Helper method to load dropdowns
        private void LoadDropDowns()
        {
            ViewBag.Companies = _context.Companies.ToList();
            ViewBag.Departments = _context.Departments.ToList();
        }
    }
}
