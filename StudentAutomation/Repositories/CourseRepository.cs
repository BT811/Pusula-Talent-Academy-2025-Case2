using Microsoft.EntityFrameworkCore;
using StudentAutomation.Data;
using StudentAutomation.Models;
using StudentAutomation.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentAutomation.Dtos;

namespace StudentAutomation.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses
                                 .Include(c => c.Teacher)          // Teacher bilgisi
                                 .Include(c => c.Enrollments)      // Enrollment bilgisi
                                     .ThenInclude(e => e.User)     // Enrollment içindeki öğrenci
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetByTeacherIdAsync(int teacherId)
        {
            return await _context.Courses
                .Where(c => c.TeacherId == teacherId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Course>> GetCoursesByTeacherIdAsync(int teacherId)
                {
                    return await _context.Courses
                        .Where(c => c.TeacherId == teacherId)
                        .Include(c => c.Enrollments)
                        .ThenInclude(e => e.User)
                        .ToListAsync();
                }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses
                                .Include(c => c.Teacher)
                                .Include(c => c.Enrollments)
                                    .ThenInclude(e => e.User)
                                .FirstOrDefaultAsync(c => c.Id == id);
        }
        

        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AssignTeacherAsync(int courseId, int teacherId)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course != null)
            {
                course.TeacherId = teacherId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateStatusAsync(int courseId, CourseStatus status)
        {
            var course = await _context.Courses.FindAsync(courseId);
            if (course != null)
            {
                course.Status = status;
                await _context.SaveChangesAsync();
            }
        }
    }
}
