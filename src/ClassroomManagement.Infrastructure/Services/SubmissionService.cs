using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
using ClassroomManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClassroomManagement.Infrastructure.Services;

public class SubmissionService : ISubmissionService
{
    private readonly AppDbContext _context;

    public SubmissionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddSubmissionAsync(Submission submission)
    {
        if (submission == null) throw new ArgumentNullException(nameof(submission), "Submission cannot be null");

        await _context.Submissions.AddAsync(submission);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Submission>> GetAllSubmissionsAsync()
    {
        return await _context.Submissions.ToListAsync();
    }

    public async Task UpdateSubmissionAsync(Submission submission)
    {
        if (submission == null) throw new ArgumentNullException(nameof(submission), "Submission cannot be null");

        _context.Submissions.Update(submission);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSubmissionAsync(string submissionID)
    {
        if (string.IsNullOrEmpty(submissionID))
            throw new ArgumentException("Submission ID cannot be null or empty", nameof(submissionID));

        var submission = await _context.Submissions.FindAsync(submissionID);
        if (submission == null) return;

        _context.Submissions.Remove(submission);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Submission>> GetSubmissionsByStudentId(string studentId)
    {
        if (string.IsNullOrEmpty(studentId))
            throw new ArgumentException("Student ID cannot be null or empty", nameof(studentId));

        return await _context.Submissions
            .Where(s => s.studentid == studentId)
            .ToListAsync();
    }

    public async Task<Submission?> GetSubmissionsByStudentIdAndAssignmentId(string studentId, string assignmentId)
    {
        return await _context.Submissions
            .Where(s => s.studentid == studentId && s.assignmentid == assignmentId)
            .FirstOrDefaultAsync();
    }
}

