﻿using Iycons_web2._0.Data;
using Iycons_web2._0.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Iycons_web2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly Context _context;

        public TagController(Context context)
        {
            _context = context;
        }

        // GET: api/Tag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            return await _context.Tags.ToListAsync();
        }

        // GET: api/Tag/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var tag = await _context.Tags.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return tag;
        }
        // GET: api/Tag/ByName/{tagName}
        [HttpGet("ByName/{tagName}")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTagsByName(string tagName)
        {
            var tags = await _context.Tags.Where(tag => tag.TagName.Contains(tagName)).ToListAsync();

            if (!tags.Any())
            {
                return NotFound();
            }

            return tags;
        }

        // POST: api/Tag
        [HttpPost]
        public async Task<ActionResult<Tag>> CreateTag(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTag), new { id = tag.TagId }, tag);
        }

        // PUT: api/Tag/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, Tag tag)
        {
            if (id != tag.TagId)
            {
                return BadRequest();
            }

            _context.Entry(tag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
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

        // DELETE: api/Tag/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.TagId == id);
        }
    }
}
