using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
using ClassroomManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClassroomManagement.Infrastructure.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddStudentAsync(Student student)
    {
        if (student == null) throw new ArgumentNullException(nameof(student));

        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _context.Students
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task UpdateStudentAsync(Student student)
    {
        if (student == null) throw new ArgumentNullException(nameof(student));

        var existingStudent = await _context.Students.FindAsync(student.studentid);
        if (existingStudent == null) return;

        existingStudent.studentname = student.studentname;
        existingStudent.studentemail = student.studentemail;
        existingStudent.studentgrade = student.studentgrade;
        existingStudent.studentbirth = student.studentbirth;

        _context.Students.Update(existingStudent);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStudentAsync(string studentId)
    {
        if (string.IsNullOrEmpty(studentId))
            throw new ArgumentException("Student ID cannot be null or empty.", nameof(studentId));

        var student = await _context.Students.FindAsync(studentId);
        if (student == null) return;

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }

    public async Task<Student?> GetStudentById(string studentId)
    {
        if (string.IsNullOrEmpty(studentId))
            throw new ArgumentException("Student ID cannot be null or empty.", nameof(studentId));

        return await _context.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.studentid == studentId);
    }

    public async Task UpdateStudentsAsync(IEnumerable<Student> students)
    {
        if (students == null) throw new ArgumentNullException(nameof(students));

        foreach (var student in students)
        {
            var existingStudent = await _context.Students.FindAsync(student.studentid);
            if (existingStudent != null)
            {
                existingStudent.studentname = student.studentname;
                existingStudent.studentemail = student.studentemail;
                existingStudent.studentgrade = student.studentgrade;
                existingStudent.studentbirth = student.studentbirth;

                _context.Students.Update(existingStudent);
            }
            else
            {
                await _context.Students.AddAsync(student);
            }
        }

        await _context.SaveChangesAsync();
    }
}

