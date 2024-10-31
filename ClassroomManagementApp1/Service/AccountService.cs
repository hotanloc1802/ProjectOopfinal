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
        //1. Read DbContext

        private readonly AppDbContext _context;

        //2. Constructor Service

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        //3. Build Service

        public async Task<Account> GetAccountByStudentID(string studentId)
        {
            return await _context.Accounts
                                 .Where(ac => ac.studentid == studentId)
                                 .FirstOrDefaultAsync();
        }


    }
}
