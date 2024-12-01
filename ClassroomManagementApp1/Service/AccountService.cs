using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomManagementApp1.ClassService
{
    public class AccountService
    {
        // 1. Read DbContext

        private readonly AppDbContext _context;

        // 2. Constructor Service

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        // 3. Build Service

        // Get account by student ID
        public async Task<Account> GetAccountByStudentID(string studentId)
        {
            return await _context.Accounts
                                 .Include(s => s.Student)
                                 .Where(ac => ac.studentid == studentId)
                                 .FirstOrDefaultAsync();
        }

        // Get profile picture by user ID
        public async Task<byte[]> GetProfilePicture(string userId)
        {
            var account = await _context.Accounts.FindAsync(userId);
            return account?.profilepicture; // Return the picture if it exists
        }

        // Update account asynchronously
        public async Task UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account); // Update the account
            await _context.SaveChangesAsync(); // Save changes to the database
        }
    }
}
