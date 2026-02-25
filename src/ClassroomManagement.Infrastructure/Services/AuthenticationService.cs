using ClassroomManagement.Application.Services;
using ClassroomManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClassroomManagement.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly AppDbContext _context;

    public AuthenticationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(string StudentId, string Role)?> AuthenticateAsync(string username, string password)
    {
        var account = await _context.Accounts
            .AsNoTracking()
            .Where(a => a.username == username && a.password == password)
            .Select(a => new { a.studentid, a.role })
            .FirstOrDefaultAsync();

        if (account == null) return null;
        return (account.studentid, account.role);
    }
}

