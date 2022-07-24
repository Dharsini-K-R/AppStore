using AppStore.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AppStore.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly UserDBContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(UserDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("[controller]/Register")]
        public async Task<IActionResult> Resgister([FromBody] Registration registration)
        {
            var userEmail = await _context.Users.FirstOrDefaultAsync(e=>e.UserName==registration.UserName);
            if (userEmail == null)
            {
                _context.Users.Add(registration);
                await _context.SaveChangesAsync();  
                return Ok("Registered Successfully");
            }
            else
            {
                return BadRequest("User Already Exist");
            }
        }
        [HttpGet]
        [Route("[controller]/getuser"),Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUser(int id)
        {
            if(id==null || _context.Users==null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            return Ok(user);
        }
        [HttpGet]
        [Route("[controller]/DeleteUser"),Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Nothing to delete");
            }
            var userModel = await _context.Users.FindAsync(id);
            if (userModel != null)
            {
                _context.Users.Remove(userModel);
                await _context.SaveChangesAsync();
                return Ok("User deleted successfully");
            }
            else
            {
                return Ok("User Not Found");
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Registration>> Login([FromBody] Registration registration)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == registration.UserName);
            if (dbUser == null)
            {
                return BadRequest("Invalid User");
            }
            else if (dbUser.Password != registration.Password)
            {
                return BadRequest("Invalid Password.");
            }
            var token = CreateToken(dbUser);
            return Ok(token);
        }

        private string CreateToken(Registration registration)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, registration.UserName),
                new Claim(ClaimTypes.Role, registration.Role),
               
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("SecretKey:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
