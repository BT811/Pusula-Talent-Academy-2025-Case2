using StudentAutomation.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentAutomation.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(int id);
        Task AssignTeacherAsync(int courseId, int teacherId);
        Task<IEnumerable<Course>> GetByTeacherIdAsync(int teacherId);
        Task UpdateStatusAsync(int courseId, CourseStatus status);
        Task<IEnumerable<Course>> GetCoursesByTeacherIdAsync(int teacherId);
    }
}
