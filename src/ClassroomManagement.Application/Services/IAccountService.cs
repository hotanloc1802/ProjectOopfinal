using ClassroomManagement.Domain.Entities;

namespace ClassroomManagement.Application.Services;

public interface IAccountService
{
    Task<Account?> GetAccountByStudentID(string studentId);

    Task<byte[]?> GetProfilePicture(string userId);

    Task UpdateAccountAsync(Account account);
}

