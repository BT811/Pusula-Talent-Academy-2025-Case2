using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentAutomation.Models;
using StudentAutomation.Services;
using StudentAutomation.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;



namespace StudentAutomation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Role bazlı kullanıcı listeleme
        [HttpGet("all")]
        [Authorize] // Token kontrolü
        public async Task<ActionResult<IEnumerable<User>>> GetAll([FromQuery] string? role = null)
        {
            // Token'dan role al
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            Console.WriteLine(userRole);

            if (userRole == null) return Unauthorized();

            IEnumerable<User> users;

            if (userRole == "Admin")
            {
                // Admin tüm kullanıcıları görebilir, role filtresi varsa uygula
                users = await _userService.GetAllAsync();
                if (!string.IsNullOrEmpty(role))
                    users = users.Where(u => u.Role == role);
            }
            else if (userRole == "Teacher")
            {
                // Teacher sadece öğrenci listesini görebilir
                users = await _userService.GetAllAsync();
                users = users.Where(u => u.Role == "Student");
            }
            else
            {
                // Student kendi bilgilerini görebilir
                var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
                var user = await _userService.GetByIdAsync(userId);
                users = new List<User> { user };
            }

            return Ok(users);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Teacher")] // Admin ve teacher güncelleyebilir
        public async Task<ActionResult> Update(int id, [FromBody] UpdateUserModel model)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            // Teacher sadece student güncelleyebilir
            if (userRole == "Teacher" && model.Role != "Student")
                return Forbid();

            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            // Kullanıcı alanlarını güncelle
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            
            user.Role = model.Role;
            
            if (!string.IsNullOrWhiteSpace(model.PasswordHash))
            {
                // password hashing işlemi
                user.PasswordHash = PasswordHelper.HashPassword(model.PasswordHash);
            }
            await _userService.UpdateAsync(user);
            return Ok();
        }

        // Tek kullanıcı çekme
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            // Role kontrolü: student kendi bilgilerini görebilir, teacher öğrenci görebilir
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userRole == "Student" && user.Id != int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value))
                return Forbid();

            if (userRole == "Teacher" && user.Role != "Student")
                return Forbid();

            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Teacher")] // Artık hem Admin hem Teacher ekleyebilir
        public async Task<ActionResult> Add(User user)
        {
            if (!string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                user.PasswordHash = PasswordHelper.HashPassword(user.PasswordHash);
            }
            await _userService.AddAsync(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Teacher")] // Artık hem Admin hem Teacher silebilir
        public async Task<ActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);
            return Ok();
        }
            }
}
