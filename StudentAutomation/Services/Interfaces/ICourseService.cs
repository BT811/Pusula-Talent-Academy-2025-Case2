using StudentAutomation.Dtos;
using StudentAutomation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAutomation.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDetailDto?> GetCourseDetailAsync(int id);
        Task<CourseDetailDto> CreateCourseAsync(CreateCourseDto createCourseDto);
        Task UpdateAsync(Course course); // Eğer DTO ile güncelleme yapılacaksa burayı da DTO ile değiştir
        Task DeleteCourseAsync(int id);
        Task AssignTeacherAsync(int courseId, int teacherId);
        Task UpdateStatusAsync(int courseId, CourseStatus status);
        Task<Course?> GetByIdAsync(int id);
        Task<IEnumerable<CourseDto>> GetCoursesByTeacherIdAsync(int teacherId);
    }
}