using ClassroomManagement.Domain.Entities;

namespace ClassroomManagement.Application.Services;

public interface ITeacherService
{
    Task AddTeacherAsync(Teacher teacher);

    Task<List<Teacher>> GetAllTeachersAsync();

    Task UpdateTeacherAsync(Teacher teacher);

    Task DeleteTeacherAsync(string teacherId);

    Task<Teacher?> GetTeacherByIdAsync(string teacherId);
}

