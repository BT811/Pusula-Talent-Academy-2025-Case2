using StudentAutomation.Models;
using System.Collections.Generic;
public interface IEnrollmentService
{
    Task<bool> EnrollStudentAsync(int courseId, int studentId);
    Task<bool> RemoveStudentAsync(int courseId, int studentId);
    Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseAsync(int courseId, int teacherId);
}
