using Iycons_web2._0.Data;
using Iycons_web2._0.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Iycons_web2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly Context _context;

        public MediaController(Context context)
        {
            _context = context;
        }

        // GET: api/Media
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Media>>> GetMedia()
        {
            return await _context.Images.ToListAsync();
        }

        // GET: api/Media/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Media>> GetMedia(int id)
        {
            var media = await _context.Images.FindAsync(id);

            if (media == null)
            {
                return NotFound();
            }

            return media;
        }

        // POST: api/Media
        [HttpPost]
        public async Task<ActionResult<Media>> CreateMedia(Media media)
        {
            _context.Images.Add(media);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedia), new { id = media.ImageId }, media);
        }

        // PUT: api/Media/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedia(int id, Media media)
        {
            if (id != media.ImageId)
            {
                return BadRequest();
            }

            _context.Entry(media).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MediaExists(id))
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

        // DELETE: api/Media/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedia(int id)
        {
            var media = await _context.Images.FindAsync(id);
            if (media == null)
            {
                return NotFound();
            }

            _context.Images.Remove(media);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MediaExists(int id)
        {
            return _context.Images.Any(e => e.ImageId == id);
        }
    }
}
