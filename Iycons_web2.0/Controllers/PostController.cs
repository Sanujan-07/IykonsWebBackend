using Iycons_web2._0.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Iycons_web2._0.Model;
using Microsoft.AspNetCore.Authorization;
using Iycons_web2._0.DTO;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using SlackAPI;
using Microsoft.AspNetCore.Authentication;


namespace Iycons_web2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PostController : ControllerBase
    {
        private readonly Context _context;
       
       
        public PostController(Context context )
        {
            _context = context;
             
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Posts>>> GetPosts()
        {
            var posts = await _context.Posts.Include(p => p.Category).Include(p => p.TagPosts).Include(p => p.Comments).Include(p => p.MediaItems).ToListAsync();

            if (posts == null)
            {
                return NotFound();
            }

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDetail>> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            // Get the media for the post, if it exists.
            var media = await _context.Medias.FindAsync(id);

            // Get the tag for the post, if it exists.
            var tag = await _context.Tags.FindAsync(id);

            // Get the comment for the post, if it exists.
            var comment = await _context.Comments.FindAsync(id);

            // Create a new post detail object.
            var postDetail = new PostDetail
            {
                PostId = post.PostId,
                Posts = post,
                media = media,
                Tag = tag,
                Comment = comment
            };

            return postDetail;
        }

        [HttpPost("create-post")]
       
        public async Task<ActionResult<Posts>> CreatePost(int categoryId,PostDto post)
        {
            
            var category = await _context.Categories.FindAsync(categoryId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (category != null )
                {
                
                var newPost = new Posts
                    {
                        Title = post.Title,
                        Description = post.Description,
                        CategoryId = categoryId,
                        UserId = 1,
                        Category = category
                        
                       
                    };

                    _context.Posts.Add(newPost);
                    await _context.SaveChangesAsync();

                    return Ok("Post Created");
                }
                else
                {
                    // Return a 404 (Not Found) response with a specific message
                    return NotFound("Category not found");
                }
            

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
