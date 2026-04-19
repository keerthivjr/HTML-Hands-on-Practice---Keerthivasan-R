using ContactManagement.API.DTOs;
using ContactManagement.API.Exceptions;
using ContactManagement.DAL.Models;
using ContactManagement.DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContactsController(IContactRepository contactRepository, ILogger<ContactsController> logger) : ControllerBase
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly ILogger<ContactsController> _logger = logger;

        [HttpGet]
        [Authorize]  // Both Admin and User can view
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await _contactRepository.GetAllContactsWithDetailsAsync();

            var contactDtos = contacts.Select(c => new ContactDto
            {
                ContactId = c.ContactId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                EmailId = c.EmailId,
                MobileNo = c.MobileNo,
                Designation = c.Designation,
                CompanyId = c.CompanyId,
                CompanyName = c.Company?.CompanyName,
                DepartmentId = c.DepartmentId,
                DepartmentName = c.Department?.DepartmentName
            });

            return Ok(contactDtos);
        }

        [HttpGet("{id}")]
        [Authorize]  // Both Admin and User can view
        public async Task<IActionResult> GetContactById(int id)
        {
            var contact = await _contactRepository.GetContactWithDetailsByIdAsync(id);

            if (contact == null)
            {
                throw new NotFoundException($"Contact with ID {id} not found");
            }

            var contactDto = new ContactDto
            {
                ContactId = contact.ContactId,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                EmailId = contact.EmailId,
                MobileNo = contact.MobileNo,
                Designation = contact.Designation,
                CompanyId = contact.CompanyId,
                CompanyName = contact.Company?.CompanyName,
                DepartmentId = contact.DepartmentId,
                DepartmentName = contact.Department?.DepartmentName
            };

            return Ok(contactDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]  // Only Admin can create
        public async Task<IActionResult> CreateContact(CreateContactDto createDto)
        {
            var contact = new ContactInfo
            {
                FirstName = createDto.FirstName,
                LastName = createDto.LastName,
                EmailId = createDto.EmailId,
                MobileNo = createDto.MobileNo,
                Designation = createDto.Designation,
                CompanyId = createDto.CompanyId,
                DepartmentId = createDto.DepartmentId
            };

            var created = await _contactRepository.AddAsync(contact);

            _logger.LogInformation("Contact created: {FirstName} {LastName} by {User}",
                created.FirstName, created.LastName, User.Identity?.Name);

            return CreatedAtAction(nameof(GetContactById), new { id = created.ContactId }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]  // Only Admin can update
        public async Task<IActionResult> UpdateContact(int id, UpdateContactDto updateDto)
        {
            var existingContact = await _contactRepository.GetByIdAsync(id);

            if (existingContact == null)
            {
                throw new NotFoundException($"Contact with ID {id} not found");
            }

            existingContact.FirstName = updateDto.FirstName;
            existingContact.LastName = updateDto.LastName;
            existingContact.EmailId = updateDto.EmailId;
            existingContact.MobileNo = updateDto.MobileNo;
            existingContact.Designation = updateDto.Designation;
            existingContact.CompanyId = updateDto.CompanyId;
            existingContact.DepartmentId = updateDto.DepartmentId;

            await _contactRepository.UpdateAsync(existingContact);

            _logger.LogInformation("Contact updated: ID {ContactId} by {User}", id, User.Identity?.Name);

            return Ok(new { message = "Contact updated successfully" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]  // Only Admin can delete
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);

            if (contact == null)
            {
                throw new NotFoundException($"Contact with ID {id} not found");
            }

            await _contactRepository.DeleteAsync(contact);

            _logger.LogInformation("Contact deleted: ID {ContactId} by {User}", id, User.Identity?.Name);

            return Ok(new { message = "Contact deleted successfully" });
        }
    }
}