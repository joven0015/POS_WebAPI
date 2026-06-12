using Microsoft.AspNetCore.Mvc;
using POS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using POS.Models;

namespace POS.Controllers
{
   

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AuthController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {

            var user = await _db.Users
       .FirstOrDefaultAsync(x => x.email == login.email);

            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(login.password_hash,user.password_hash))
                {
                    return Ok("Logged in as " + login.email);
                }
                else
                    return Unauthorized("Login failed");
            }
            return Unauthorized("Invalid email or password");
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exists = await _db.Users
                .AnyAsync(x => x.email == register.email);

            if (exists)
                return Conflict(new { Message = "Email already exists." });

            var user = new User
            {
                email = register.email,
                password_hash = BCrypt.Net.BCrypt.HashPassword(register.password_hash),
                role = register.role,
                created_at = DateTime.UtcNow
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Register), new
            {
                user.id,
                user.email,
                user.role,
                user.created_at
            });
        }
    }
}


