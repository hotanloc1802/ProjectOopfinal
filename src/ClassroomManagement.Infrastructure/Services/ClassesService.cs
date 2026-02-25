using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
using ClassroomManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClassroomManagement.Infrastructure.Services;

public class ClassesService : IClassesService
{
    private readonly AppDbContext _context;

    public ClassesService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Class>> GetAllClassesByStudentId(string studentId)
    {
        return await _context.Classes
            .Where(c => c.ClassStudents.Any(cs => cs.studentid == studentId))
            .Include(c => c.Teacher)
            .Include(c => c.Subject)
            .ToListAsync();
    }

    public async Task<List<Class>> GetTop3NearestClassesByStudentId(string studentId)
    {
        return await _context.Classes
            .Join(_context.ClassStudent,
                c => c.classid,
                cs => cs.classid,
                (c, cs) => new { Class = c, ClassStudent = cs })
            .Where(cs => cs.ClassStudent.studentid == studentId)
            .OrderBy(c => c.Class.datebegin)
            .Select(cs => cs.Class)
            .Include(c => c.Teacher)
            .Include(c => c.Subject)
            .Include(c => c.Assignments)
            .Take(3)
            .ToListAsync();
    }

    public async Task<Class?> GetClassById(string classId)
    {
        return await _context.Classes
            .Include(c => c.Teacher)
            .Include(c => c.Subject)
            .Include(c => c.Assignments)
            .FirstOrDefaultAsync(c => c.classid == classId);
    }

    public async Task<List<Class>> GetAllClassesAsync()
    {
        return await _context.Classes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Class>> GetClassesByStudentId(string studentId)
    {
        return await _context.Classes
            .Where(c => c.ClassStudents.Any(cs => cs.studentid == studentId))
            .Include(c => c.Teacher)
            .Include(c => c.Subject)
            .Include(c => c.Assignments)
            .ToListAsync();
    }
}

