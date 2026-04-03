using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System.Linq;

namespace AppUILayer.Controllers
{
    [Route("contacts")]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [Route("")]
        [Route("show")]
        public IActionResult ShowContacts()
        {
            var contacts = _contactRepository.GetAllContacts();
            return View(contacts);
        }

        [Route("details/{id}")]
        public IActionResult GetContactById(int id)
        {
            var contact = _contactRepository.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        [Route("add")]
        [HttpGet]
        public IActionResult AddContact()
        {
            ViewBag.Companies = _contactRepository.GetAllCompanies()
                .Select(c => new { c.CompanyId, c.CompanyName }).ToList();
            ViewBag.Departments = _contactRepository.GetAllDepartments()
                .Select(d => new { d.DepartmentId, d.DepartmentName }).ToList();
            return View();
        }

        [Route("add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddContact(ContactInfo contact)
        {
            if (ModelState.IsValid)
            {
                _contactRepository.AddContact(contact);
                return RedirectToAction("ShowContacts");
            }

            ViewBag.Companies = _contactRepository.GetAllCompanies()
                .Select(c => new { c.CompanyId, c.CompanyName }).ToList();
            ViewBag.Departments = _contactRepository.GetAllDepartments()
                .Select(d => new { d.DepartmentId, d.DepartmentName }).ToList();
            return View(contact);
        }

        [Route("edit/{id}")]
        [HttpGet]
        public IActionResult EditContact(int id)
        {
            var contact = _contactRepository.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }

            ViewBag.Companies = _contactRepository.GetAllCompanies()
                .Select(c => new { c.CompanyId, c.CompanyName }).ToList();
            ViewBag.Departments = _contactRepository.GetAllDepartments()
                .Select(d => new { d.DepartmentId, d.DepartmentName }).ToList();
            return View(contact);
        }

        [Route("edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditContact(ContactInfo contact)
        {
            if (ModelState.IsValid)
            {
                _contactRepository.UpdateContact(contact);
                return RedirectToAction("ShowContacts");
            }

            ViewBag.Companies = _contactRepository.GetAllCompanies()
                .Select(c => new { c.CompanyId, c.CompanyName }).ToList();
            ViewBag.Departments = _contactRepository.GetAllDepartments()
                .Select(d => new { d.DepartmentId, d.DepartmentName }).ToList();
            return View(contact);
        }

        [Route("delete/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteContact(int id)
        {
            _contactRepository.DeleteContact(id);
            return RedirectToAction("ShowContacts");
        }
    }
}