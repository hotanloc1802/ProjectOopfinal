using ClassroomManagement.Domain.Entities;

namespace ClassroomManagement.Application.Services;

public interface IAssignmentService
{
    Task<List<Assignment>> GetAllAssignment();

    Task<List<Assignment>> GetAssignmentByClassID(string classId);

    Task<Assignment?> GetNearestAssignmentByClassID(string classId);

    Task<List<Assignment>> GetAssignmentsByStudentId(string studentId);
}

