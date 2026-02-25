using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
using ClassroomManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClassroomManagement.Infrastructure.Services;

public class AssignmentService : IAssignmentService
{
    private readonly AppDbContext _context;

    public AssignmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Assignment>> GetAllAssignment()
    {
        var assignments = await _context.Assignment
            .Include(a => a.Class)
            .ToListAsync();

        foreach (var assignment in assignments)
        {
            assignment.UpdateStatus();
        }

        return assignments;
    }

    public async Task<List<Assignment>> GetAssignmentByClassID(string classId)
    {
        var assignments = await _context.Assignment
            .Where(a => a.classid == classId)
            .OrderBy(a => a.duedate)
            .ToListAsync();

        foreach (var assignment in assignments)
        {
            assignment.UpdateStatus();
        }

        return assignments;
    }

    public async Task<Assignment?> GetNearestAssignmentByClassID(string classId)
    {
        var assignment = await _context.Assignment
            .Where(a => a.classid == classId)
            .OrderBy(a => a.duedate)
            .FirstOrDefaultAsync();

        assignment?.UpdateStatus();
        return assignment;
    }

    public async Task<List<Assignment>> GetAssignmentsByStudentId(string studentId)
    {
        var assignments = await _context.Assignment
            .Where(a => a.Class.ClassStudents.Any(cs => cs.studentid == studentId))
            .Include(a => a.Class)
            .OrderBy(a => a.duedate)
            .ToListAsync();

        foreach (var assignment in assignments)
        {
            assignment.UpdateStatus();
        }

        return assignments;
    }
}

