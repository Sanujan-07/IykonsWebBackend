using Iycons_web2._0.DTO;
using Iycons_web2._0.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Iycons_web2._0.Data;
using Microsoft.EntityFrameworkCore;
using Iycons_web2._0.Service;

namespace Iycons_web2._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static User user = new User();
        private readonly Context _context;
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration, Context context)
        {
            _configuration = configuration;
            _context = context; 
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTO request)
        {
            // Check if the user already exists
            if (_context.Users.Any(u => u.UserName == request.UserName || u.Email == request.Email))
            {
                return BadRequest("Username or email already taken");
            }

            // Create a new user object
            var user = new User
            {
                UserName = request.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Email = request.Email
            };

            // Add the new user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Return the new user
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginDto request)
        {
            // Find the email for database 
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user.Email != request.Email)
            {
                return BadRequest("User Not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Password does not match");
            }

            var tokenService = new TokenService(_configuration);
            string token = tokenService.CreateToken(user);
            return Ok(token);
        }

      
    }
}
