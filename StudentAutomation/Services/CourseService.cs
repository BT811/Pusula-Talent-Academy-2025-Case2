using StudentAutomation.Models;
using StudentAutomation.Repositories.Interfaces;
using StudentAutomation.Services.Interfaces;
using StudentAutomation.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAutomation.Dtos;

namespace StudentAutomation.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Name = c.Name,
                TeacherName = c.Teacher != null ? $"{c.Teacher.FirstName} {c.Teacher.LastName}" : "Unassigned",
                Status = c.Status.ToString()
            });
        }

        public async Task<CourseDetailDto?> GetCourseDetailAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return null;

            return new CourseDetailDto
            {
                Id = course.Id,
                Name = course.Name,
                TeacherName = course.Teacher != null ? $"{course.Teacher.FirstName} {course.Teacher.LastName}" : "Unassigned",
                Status = course.Status.ToString(),
                EnrolledStudents = course.Enrollments?.Select(e => $"{e.User.FirstName} {e.User.LastName}").ToList() ?? new List<string>()
            };
        }
        public async Task<IEnumerable<CourseDto>> GetCoursesByTeacherIdAsync(int teacherId)
        {
            var courses = await _courseRepository.GetCoursesByTeacherIdAsync(teacherId);
            return courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Name = c.Name,
                TeacherName = c.Teacher != null ? $"{c.Teacher.FirstName} {c.Teacher.LastName}" : "Unassigned",
                Status = c.Status.ToString()
            });
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _courseRepository.GetByIdAsync(id);
        }
    
        public async Task<CourseDetailDto> CreateCourseAsync(CreateCourseDto createCourseDto)
        {
            var course = new Course
            {
                Name = createCourseDto.Name,
                TeacherId = createCourseDto.TeacherId,
                Status = CourseStatus.NotStarted
            };

            await _courseRepository.AddAsync(course);
            return await GetCourseDetailAsync(course.Id) ?? throw new System.Exception("Course creation failed");
        }
        

        public async Task UpdateAsync(Course course)
        {
            await _courseRepository.UpdateAsync(course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            await _courseRepository.DeleteAsync(id);
        }

        public async Task AssignTeacherAsync(int courseId, int teacherId)
        {
            await _courseRepository.AssignTeacherAsync(courseId, teacherId);
        }

        public async Task UpdateStatusAsync(int courseId, CourseStatus status)
        {
            await _courseRepository.UpdateStatusAsync(courseId, status);
        }
    }
}
