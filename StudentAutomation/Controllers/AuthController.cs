using Microsoft.AspNetCore.Mvc;
using StudentAutomation.Models;
using StudentAutomation.Repositories;
using StudentAutomation.Services;
using StudentAutomation.Repositories.Interfaces;
using StudentAutomation.Services.Interfaces;

namespace StudentAutomation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;

        public AuthController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        
        {
            // şifreyi hashle
            user.PasswordHash = PasswordHelper.HashPassword(user.PasswordHash);

            var result = await _userService.RegisterUser(user);
            if (!result)
                return BadRequest("Bu email zaten kayıtlı.");

            return Ok(new { message = "Kullanıcı kaydedildi" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userService.GetByEmailAsync(loginDto.Email);
            if (user == null)
                return Unauthorized("Kullanıcı bulunamadı");

            bool isPasswordValid = PasswordHelper.VerifyPassword(user.PasswordHash, loginDto.Password);
            if (!isPasswordValid)
                return Unauthorized("Geçersiz şifre");

            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Email, user.Role);

            return Ok(new LoginResponse
                {
                    Token = token,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role
                });
        }
    }
}
