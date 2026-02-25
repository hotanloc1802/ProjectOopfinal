using ClassroomManagement.Domain.Entities;

namespace ClassroomManagement.Application.Services;

public interface ISubmissionService
{
    Task AddSubmissionAsync(Submission submission);

    Task<List<Submission>> GetAllSubmissionsAsync();

    Task UpdateSubmissionAsync(Submission submission);

    Task DeleteSubmissionAsync(string submissionID);

    Task<List<Submission>> GetSubmissionsByStudentId(string studentId);

    Task<Submission?> GetSubmissionsByStudentIdAndAssignmentId(string studentId, string assignmentId);
}

