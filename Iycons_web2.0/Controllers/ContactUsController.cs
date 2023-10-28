
using Iycons_web2._0.Data;
using Iycons_web2._0.DTO;
using Iycons_web2._0.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactUs = Iycons_web2._0.Model.ContactUs;

namespace Iycons_web2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly Context _context;

        public ContactUsController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactUs>>> GetContactUs()
        {
            return await _context.ContactUs.ToListAsync();
        }

        // GET: api/Feedback/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactUs>> GetContactUs(int id)
        {
            var contactUs = await _context.ContactUs.FindAsync(id);

            if (contactUs == null)
            {
                return NotFound();
            }

            return contactUs;
        }

        // POST: api/Feedback
        [HttpPost]
        public async Task<ActionResult<ContactUs>> PostContactUs(ContactUsDto contactDto)
        {
            var contactUs = new ContactUs
            {
                Name = contactDto.Name,
                Message = contactDto.Message,
                Email = contactDto.Email,
                Subject = contactDto.Subject,
                
            };

            _context.ContactUs.Add(contactUs);
            await _context.SaveChangesAsync();

            return Ok(contactUs);
        }

        // PUT: api/Feedback/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedback(int id, ContactUs contactUs)
        {
            if (id != contactUs.ContactId)
            {
                return BadRequest();
            }

            var contact = await _context.ContactUs.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            // Update the feedback object with values from the DTO
            contact.Name = contactUs.Name;
            contact.Message = contactUs.Message;
            contact.Email = contactUs.Email;
            contact.Subject = contactUs.Subject;
            

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Feedback/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContactUs>> DeleteFeedback(int id)
        {
            var contactUs = await _context.ContactUs.FindAsync(id);
            if (contactUs == null)
            {
                return NotFound();
            }

            _context.ContactUs.Remove(contactUs);
            await _context.SaveChangesAsync();

            return contactUs;
        }

        private bool FeedbackExists(int id)
        {
            return _context.ContactUs.Any(e => e.ContactId == id);
        }
    }
}
