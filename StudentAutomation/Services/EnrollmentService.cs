using StudentAutomation.Models;
using StudentAutomation.Repositories.Interfaces;
using StudentAutomation.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICourseRepository _courseRepository; // course kontrolü için lazım

    public EnrollmentService(IEnrollmentRepository enrollmentRepository, ICourseRepository courseRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _courseRepository = courseRepository;
    }

    public async Task<bool> EnrollStudentAsync(int courseId, int studentId)
    {
        var course = await _courseRepository.GetByIdAsync(courseId);
        if (course == null) return false;                 // Course yoksa
        

        var enrollments = await _enrollmentRepository.GetByCourseIdAsync(courseId);
        if (enrollments.Any(e => e.UserId == studentId)) return false; // Öğrenci zaten varsa

        var enrollment = new Enrollment
        {
            CourseId = courseId,
            UserId = studentId
        };

        await _enrollmentRepository.AddAsync(enrollment);
        await _enrollmentRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveStudentAsync(int courseId, int studentId)
    {
        var course = await _courseRepository.GetByIdAsync(courseId);
        if (course == null) return false;                 
         

        var enrollment = (await _enrollmentRepository.GetByCourseIdAsync(courseId))
                        .FirstOrDefault(e => e.UserId == studentId);

        if (enrollment == null) return false;  // Öğrenci derste yok

        await _enrollmentRepository.DeleteAsync(enrollment);
        await _enrollmentRepository.SaveChangesAsync();
        return true;
    }


    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseAsync(int courseId, int teacherId)
    {
        var course = await _courseRepository.GetByIdAsync(courseId);
        if (course == null || course.TeacherId != teacherId)
            return Enumerable.Empty<Enrollment>();

        return await _enrollmentRepository.GetByCourseIdAsync(courseId);
    }
}
