using ExpenseDataAccessLayer.Models;
using ExpenseWebApi.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseWebApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized("Invalid credentials");

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (!result.Succeeded) return Unauthorized("Invalid login attempt");

            return Ok("Login successful");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out successfully");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new User()
            {
                UserName = model.Username,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { message = "User registered successfully" });
        }

        [HttpGet("access-denied")]
        public IActionResult AccessDenied()
        {
            return StatusCode(StatusCodes.Status403Forbidden, "You are not authorized to access this resource.");
        }
    }
}
