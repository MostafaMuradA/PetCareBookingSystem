using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PetCareBookingSystem.DTOs.AccountsDTO;
using PetCareBookingSystem.Models;
using PetCareBookingSystem.Constants;
using PetCareBookingSystem.Services;

namespace PetCareBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IUserService userService;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userService = userService;
        }

        private async Task<string> GenerateToken(User user)
        {
            var roles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName ?? "")
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SecretKEY For TOKen Authentication"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                return BadRequest(new { message = "Email is already registered." });
            }

            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.UserName
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            
            await userManager.AddToRoleAsync(user, Roles.Customer);
            var roles = await userManager.GetRolesAsync(user);

            return Ok(new UserDTO()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? Roles.Customer,
                Token = await GenerateToken(user)
            });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized(new { message = "Invalid email or password" });

            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new { message = "Invalid email or password" });

            var roles = await userManager.GetRolesAsync(user);

            return Ok(new UserDTO()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? Roles.Customer,
                Token = await GenerateToken(user)
            });
        }

        [HttpGet("roles/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IList<string>>> GetUserRoles(string userId)
        {
            var roles = await userService.GetUserRolesAsync(userId);
            return Ok(roles);
        }

        [HttpPost("roles/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddUserToRole(string userId, [FromBody] string role)
        {
            if (!Roles.All.Contains(role))
            {
                return BadRequest(new { message = "Invalid role" });
            }

            var result = await userService.AddToRoleAsync(userId, role);
            if (!result)
            {
                return BadRequest(new { message = "Failed to add role to user" });
            }

            return Ok();
        }

        [HttpDelete("roles/{userId}/{role}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RemoveUserFromRole(string userId, string role)
        {
            if (!Roles.All.Contains(role))
            {
                return BadRequest(new { message = "Invalid role" });
            }

            var result = await userService.RemoveFromRoleAsync(userId, role);
            if (!result)
            {
                return BadRequest(new { message = "Failed to remove role from user" });
            }

            return Ok();
        }
    }
}
