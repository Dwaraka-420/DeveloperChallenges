using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DeveloperChallenges.Services;
using DeveloperChallenges.Models;

namespace GreenPtintmini.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;

        public AuthController(UserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegistrationDto registrationDto)
        {
            await _userService.RegisterUserAsync(
                registrationDto.Username,
                registrationDto.Password,
                registrationDto.Email,
                registrationDto.Role
            );
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var user = await _userService.AuthenticateUserAsync(loginDto.Username, loginDto.Password);
            if (user == null)
            {
                // Return false with error message if authentication fails
                return Ok(new { Success = false, Message = "Invalid credentials." });
            }

            // Generate the token for the authenticated user
            var token = _tokenService.GenerateToken(user);

            // Return true with the token if authentication succeeds
            return Ok(new { Success = true, Token = token });
        }

    }
}
