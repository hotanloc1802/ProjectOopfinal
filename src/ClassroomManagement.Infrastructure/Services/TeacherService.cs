using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
using ClassroomManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClassroomManagement.Infrastructure.Services;

public class TeacherService : ITeacherService
{
    private readonly AppDbContext _context;

    public TeacherService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddTeacherAsync(Teacher teacher)
    {
        await _context.Teachers.AddAsync(teacher);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Teacher>> GetAllTeachersAsync()
    {
        return await _context.Teachers.ToListAsync();
    }

    public async Task UpdateTeacherAsync(Teacher teacher)
    {
        _context.Teachers.Update(teacher);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTeacherAsync(string teacherId)
    {
        var teacher = await _context.Teachers.FindAsync(teacherId);
        if (teacher == null) return;

        _context.Teachers.Remove(teacher);
        await _context.SaveChangesAsync();
    }

    public async Task<Teacher?> GetTeacherByIdAsync(string teacherId)
    {
        return await _context.Teachers.FirstOrDefaultAsync(t => t.teacherid == teacherId);
    }
}

