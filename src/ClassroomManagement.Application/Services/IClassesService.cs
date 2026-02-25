using ClassroomManagement.Domain.Entities;

namespace ClassroomManagement.Application.Services;

public interface IClassesService
{
    Task<List<Class>> GetAllClassesByStudentId(string studentId);

    Task<List<Class>> GetTop3NearestClassesByStudentId(string studentId);

    Task<Class?> GetClassById(string classId);

    Task<List<Class>> GetAllClassesAsync();

    Task<List<Class>> GetClassesByStudentId(string studentId);
}

