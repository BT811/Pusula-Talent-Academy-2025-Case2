using StudentAutomation.Models;
using StudentAutomation.Data;

public interface IEnrollmentRepository
{
    Task<Enrollment?> GetByIdAsync(int id);
    Task<IEnumerable<Enrollment>> GetByCourseIdAsync(int courseId);
    Task AddAsync(Enrollment enrollment);
    Task DeleteAsync(Enrollment enrollment);
    Task SaveChangesAsync();
}