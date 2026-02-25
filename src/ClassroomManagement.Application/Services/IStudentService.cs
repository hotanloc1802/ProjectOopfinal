using ClassroomManagement.Domain.Entities;

namespace ClassroomManagement.Application.Services;

public interface IStudentService
{
    Task AddStudentAsync(Student student);

    Task<List<Student>> GetAllStudentsAsync();

    Task UpdateStudentAsync(Student student);

    Task DeleteStudentAsync(string studentId);

    Task<Student?> GetStudentById(string studentId);

    Task UpdateStudentsAsync(IEnumerable<Student> students);
}

