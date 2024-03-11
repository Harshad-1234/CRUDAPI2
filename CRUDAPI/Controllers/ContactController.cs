using CRUDAPI.Data;
using CRUDAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly ContactDBContext _dbContext;
        public ContactController(ContactDBContext contactDBContext)
        {
            _dbContext = contactDBContext;
        }


        [HttpGet]
        public IEnumerable<Contact> GetContacts()
        {

            return _dbContext.Contacts.ToList();
        }



        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {

            var contact = await _dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                
                return Ok(contact);

            }

            return NotFound();
        }


        [HttpPost]
        public async Task<Contact> AddContacts(AddContacts addContactsRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                FullName = addContactsRequest.FullName,
                Email = addContactsRequest.Email,
                Address = addContactsRequest.Address,
                Phone = addContactsRequest.Phone
               
            };

           await _dbContext.Contacts.AddAsync(contact);
           await _dbContext.SaveChangesAsync();
           
            return contact;
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContacts([FromRoute] Guid id, UpdateContact updateContactsRequest)
        {

            var contact =await _dbContext.Contacts.FindAsync(id);

            if(contact != null)
            {
                contact.FullName = updateContactsRequest.FullName;
                contact.Email = updateContactsRequest.Email;
                contact.Address = updateContactsRequest.Address;
                contact.Phone = updateContactsRequest.Phone;
                

                await _dbContext.SaveChangesAsync();

                return Ok(contact);

            }

            return NotFound();
        }



        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {

            var contact = await _dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {

                _dbContext.Remove(contact);
                await _dbContext.SaveChangesAsync();

                return Ok(contact);

            }

            return NotFound();
        }
    }
}
