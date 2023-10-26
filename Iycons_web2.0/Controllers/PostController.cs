﻿using Iycons_web2._0.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Iycons_web2._0.Model;
using Microsoft.AspNetCore.Authorization;
using Azure.Core;
using Microsoft.Extensions.Hosting;
using Iycons_web2._0.DTO;

namespace Iycons_web2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PostController : ControllerBase
    {
        private readonly Context _context;

        public PostController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Posts>>> GetPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Posts>> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        [HttpPost("create-post")]
        
        public async Task<ActionResult<Posts>> CreatePost(PostDto post)
        {
           
            var posts = new Posts
            {
                Title = post.Title,
                Description = post.Description,
                
                // Associate the post with the user
            };

            // Save the post to the database.
            _context.Posts.Add(posts);
            _context.SaveChanges();

            // Return the created post or a success message.
            return Ok(posts);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, Posts post)
        {
            if (id != post.PostId)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }
    }
}
