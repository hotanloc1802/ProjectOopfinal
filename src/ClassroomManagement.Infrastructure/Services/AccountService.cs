using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
using ClassroomManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClassroomManagement.Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly AppDbContext _context;

    public AccountService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetAccountByStudentID(string studentId)
    {
        return await _context.Accounts
            .Include(s => s.Student)
            .Where(ac => ac.studentid == studentId)
            .FirstOrDefaultAsync();
    }

    public async Task<byte[]?> GetProfilePicture(string userId)
    {
        var account = await _context.Accounts.FindAsync(userId);
        return account?.profilepicture;
    }

    public async Task UpdateAccountAsync(Account account)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
    }
}

