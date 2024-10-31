using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassroomManagementApp1.ClassService
{
    public class AssignmentService
    {
        //1. Read DbContext

        private readonly AppDbContext _context;

        //2. Constructor Service

        public AssignmentService(AppDbContext context)
        {
            _context = context;
        }
        //3. Build Service

        // Lấy tất cả Assignment
        public async Task<List<Assignment>> GetAllAssignment()
        {
            return await _context.Assignment
                                 .Include(a => a.Class)  // Include để lấy thông tin liên quan đến Class
                                 .ToListAsync();
        }

        // Lấy Assignment theo ClassID
        public async Task<List<Assignment>> GetAssignmentByClassID(string classId)
        {
            return await _context.Assignment
                                 .Where(a => a.classid == classId)  // Điều kiện lọc theo classId
                                 .OrderBy(a => a.duedate)  // Sắp xếp theo DueDate tăng dần
                                 .ToListAsync();
        }

        // Lấy Assignment gần đến hạn nhất theo classid
        public async Task<Assignment> GetNearestAssignmentByClassID(string classId)
        {
            return await _context.Assignment
                                 .Where(a => a.classid == classId)  // Điều kiện lọc theo classId
                                 .OrderBy(a => a.duedate)  // Sắp xếp theo DueDate tăng dần
                                 .FirstOrDefaultAsync(); // Lấy bản ghi đầu tiên (có DueDate gần nhất)
        }

        // Lấy danh sách assignment của học sinh theo studentId
        public async Task<List<Assignment>> GetAssignmentsByStudentId(string studentId)
        {
            return await _context.Assignment
                                 .Where(a => a.Class.ClassStudents.Any(cs => cs.studentid == studentId)) // Lọc theo studentId
                                 .OrderBy(a => a.duedate) // Sắp xếp theo DueDate tăng dần
                                 .ToListAsync();
        }

    }
}
