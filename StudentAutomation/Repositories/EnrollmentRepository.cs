using StudentAutomation.Models;
using Microsoft.EntityFrameworkCore;
using StudentAutomation.Data;
using StudentAutomation.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentAutomation.Dtos;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly AppDbContext _context;
    public EnrollmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Enrollment?> GetByIdAsync(int id)
    {
        return await _context.Enrollments
            .Include(e => e.User)
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Enrollment>> GetByCourseIdAsync(int courseId)
    {
        return await _context.Enrollments
            .Include(e => e.User)
            .Where(e => e.CourseId == courseId)
            .ToListAsync();
    }

    public async Task AddAsync(Enrollment enrollment)
    {
        await _context.Enrollments.AddAsync(enrollment);
    }

    public async Task DeleteAsync(Enrollment enrollment)
    {
        _context.Enrollments.Remove(enrollment);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
