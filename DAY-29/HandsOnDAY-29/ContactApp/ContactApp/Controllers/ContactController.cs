using ContactApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Controllers
{
    public class ContactController : Controller
    {
        // In memory list
        private static List<ContactInfo> contacts = new List<ContactInfo>
        {
            new ContactInfo { ContactId = 1, FirstName = "Sakshi", LastName = "Jadhav", CompanyName = "Cognizant", EmailId = "sakshi@gmail.com", MobileNo = 8765463534, Designation = "Software Engineer" },
            new ContactInfo { ContactId = 2, FirstName = "Mahi", LastName = "Reddy", CompanyName = "Capgemini", EmailId = "mahi@gmail.com", MobileNo = 9876764337, Designation = "Frontend Developer" }
        };

        // SHOW ALL CONTACTS
        public ActionResult ShowContacts()
        {
            return View(contacts);
        }

        // GET CONTACT BY ID
        public ActionResult GetContactById(int id)
        {
            var contact = contacts.FirstOrDefault(c => c.ContactId == id);
            if (contact == null)
            {
                return Content("Contact Not found!");
            }
            return View(contact);
        }

        // LOAD ADD CONTACT PAGE
        public ActionResult AddContact()
        {
            return View();
        }

        // SAVE CONTACT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddContact(ContactInfo contact)
        {
            if (ModelState.IsValid)
            {
                // Check if ID already exists (since we don't have a DB to handle unique keys)
                if (contacts.Any(c => c.ContactId == contact.ContactId))
                {
                    ModelState.AddModelError("ContactId", "This ID is already taken!");
                    return View(contact);
                }

                contacts.Add(contact);
                return RedirectToAction("ShowContacts");
            }
            return View(contact);
        }
    }
}
