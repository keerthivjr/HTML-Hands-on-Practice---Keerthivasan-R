using ContactAppDI.Models;
using ContactAppDI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactAppDI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _service;

        public ContactController(IContactService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var contacts = _service.GetContacts();
            return View(contacts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Contact contact)
        {
            _service.AddContact(contact);
            return RedirectToAction("Index");
        }
    }
}
