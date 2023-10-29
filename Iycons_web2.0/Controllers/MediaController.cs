using Amazon.S3.Model;
using Iycons_web2._0.Data;
using Iycons_web2._0.DTO;
using Iycons_web2._0.Model;
using Iycons_web2._0.Service;
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
        private readonly ImageStorageService _imageStorageService;
        public MediaController(Context context, ImageStorageService imageStorageService)
        {
            _context = context;
            _imageStorageService = imageStorageService;
        }

        // GET: api/Media
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Media>>> GetMedia()
        {
            return await _context.Medias.ToListAsync();
        }

        // GET: api/Media/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Media>> GetMedia(int id)
        {
            var media = await _context.Medias.FindAsync(id);

            if (media == null)
            {
                return NotFound();
            }

            return media;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile imageFile)
        {
            var imageModel = await _imageStorageService.SaveImage(imageFile);

            if (imageModel != null)
            {
                return Ok(new { imagePath = imageModel.FilePath });
            }

            return BadRequest("Failed to upload the image.");
        }
        // POST: api/Media
        /*[HttpPost]
        public async Task<ActionResult<Media>> CreateMedia(MediaDto mediaDto)
        {
            var media = new Media
            {
                Filname = mediaDto.Filname,
                Path_Name = mediaDto.Path_Name

            };
            _context.Medias.Add(media);
            await _context.SaveChangesAsync();

            return Ok(media);
        }
        */
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
            var media = await _context.Medias.FindAsync(id);
            if (media == null)
            {
                return NotFound();
            }

            _context.Medias.Remove(media);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MediaExists(int id)
        {
            return _context.Medias.Any(e => e.ImageId == id);
        }
    }
}
