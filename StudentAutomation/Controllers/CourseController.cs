using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentAutomation.Dtos;
using StudentAutomation.Models;
using StudentAutomation.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;

namespace StudentAutomation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IEnrollmentService _enrollmentService;

        public CourseController(ICourseService courseService, IEnrollmentService enrollmentService)
        {
            _courseService = courseService;
            _enrollmentService = enrollmentService;
        }

        // 🔹 Tüm dersleri listele (DTO)
        [HttpGet]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAll()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        // 🔹 Tek ders detayları (DTO)
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<ActionResult<CourseDetailDto>> GetById(int id)
        {
            var course = await _courseService.GetCourseDetailAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CourseDetailDto>> Add([FromBody] CreateCourseDto createCourseDto)
        {

            if (createCourseDto.TeacherId == 0)
                createCourseDto.TeacherId = null;

            var course = await _courseService.CreateCourseAsync(createCourseDto);

            return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
        }

        // 🔹 Ders güncelleme (Admin)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(int id, [FromBody] CourseUpdateDto dto)
        {
            if (dto.TeacherId == 0)
                dto.TeacherId = null;

            var course = await _courseService.GetByIdAsync(id);
            if (course == null) return NotFound();

            if (dto.Name != null) course.Name = dto.Name;
            if (dto.TeacherId.HasValue) course.TeacherId = dto.TeacherId.Value;
            if (dto.Status.HasValue) course.Status = dto.Status.Value;

            await _courseService.UpdateAsync(course);
            return Ok(new { message = "Course updated successfully" });
        }

        // 🔹 Ders silme (Admin)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return Ok(new { message = "Course deleted successfully" });
        }

        // 🔹 Öğretmeni derse atama (Admin)
        [HttpPut("assign-teacher/{courseId}/{teacherId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AssignTeacher(int courseId, int teacherId)
        {
            await _courseService.AssignTeacherAsync(courseId, teacherId);
            return Ok(new { message = "Teacher assigned successfully" });
        }


        [HttpGet("my-courses")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetMyCourses()
        {
            var teacherIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (string.IsNullOrEmpty(teacherIdClaim))
                return Unauthorized();

            var teacherId = int.Parse(teacherIdClaim);

            var courses = await _courseService.GetCoursesByTeacherIdAsync(teacherId);
            return Ok(courses);
        }

        // 🔹 Ders statüsünü değiştirme (Teacher)
        [HttpPut("update-status/{courseId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> UpdateStatus(int courseId, [FromBody] CourseStatus status)
        {
            await _courseService.UpdateStatusAsync(courseId, status);
            return Ok(new { message = "Course status updated successfully" });
        }
        
        [HttpPost("{courseId}/enroll/{studentId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> EnrollStudent(int courseId, int studentId)
        {
            

            var success = await _enrollmentService.EnrollStudentAsync(courseId,studentId);
            if (!success) return Forbid();  // Course yoksa, Teacher değilse veya öğrenci zaten varsa
            return Ok(new { message = "Student enrolled successfully" });
        }

        [HttpDelete("{courseId}/enroll/{studentId}")]
        [Authorize(Roles = "Teacher")]
        public async Task<ActionResult> RemoveStudent(int courseId, int studentId)
        {
            

            var success = await _enrollmentService.RemoveStudentAsync(courseId,studentId);
            if (!success) return Forbid();  // Course yoksa, Teacher değilse veya enrollment yoksa
            return Ok(new { message = "Student removed successfully" });
        }
    }
}
