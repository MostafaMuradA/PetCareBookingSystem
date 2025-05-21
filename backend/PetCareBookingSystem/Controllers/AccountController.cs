using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetCareBookingSystem.DTOs.AccountsDTO;
using PetCareBookingSystem.Models;

namespace PetCareBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager , 
                                 SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        //Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
        {
            var user = new User() 
            { 
                FullName = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
           var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest();
            return Ok();
        }

        //Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if(user is null) return Unauthorized();   
           var result  = await signInManager.CheckPasswordSignInAsync(user,model.Password,false);
            if (!result.Succeeded) return BadRequest(); 
            return Ok();
        }

    }
}
