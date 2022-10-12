using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication6.DataAccess;
using WebApplication6.Entity;
using WebApplication6.Entity.Models.Dtos;

namespace WebApplication6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public ContactController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await databaseContext.Contacts.ToListAsync());
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)

        {
            var contact = await databaseContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return BadRequest(contact);
            }
            else
            {
                return Ok(contact);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostContact(AddContactRequest addContact)
        {
            Contact contact = new Contact
            {
                Id = Guid.NewGuid(),
                FullName = addContact.FullName,
                Adress = addContact.Adress,
                Email = addContact.Email,
                Phone = addContact.Phone,
            };
            await databaseContext.Contacts.AddAsync(contact);
            await databaseContext.SaveChangesAsync();

            return Ok(contact);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContact)
        {
            var contact = await databaseContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                contact.FullName = updateContact.FullName;
                contact.Adress = updateContact.Adress;
                contact.Phone = updateContact.Phone;
                contact.Email = updateContact.Email;
                await databaseContext.SaveChangesAsync();
                return Ok(contact);

            }
            return NotFound();




        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = databaseContext.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }
            else
            {
                databaseContext.Remove(contact);
                await databaseContext.SaveChangesAsync();
                return Ok(contact);
            }

        }
    }
}