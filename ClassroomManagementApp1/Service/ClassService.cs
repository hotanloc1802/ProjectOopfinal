using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassroomManagementApp1.ClassService
{
    public class ClassesService
    {
        //1. Read DbContext

        private readonly AppDbContext _context;

        //2.Constructor Service

        public ClassesService(AppDbContext context)
        {
            _context = context;
        }

        //3. Build Service

        // Lấy tất cả các lớp học theo studentId (bao gồm giáo viên và môn học)
        public async Task<List<Class>> GetAllClassesByStudentId(string studentId)
        {
            return await _context.Classes
                .Where(c => c.ClassStudents.Any(cs => cs.studentid == studentId)) // Lọc theo studentId
                .Include(c => c.Teacher) // Bao gồm thông tin giáo viên
                .Include(c => c.Subject) // Bao gồm thông tin môn học
                .ToListAsync();
        }

        // Lấy 3 lớp học gần nhất theo ngày bắt đầu (bao gồm giáo viên và môn học)
        public async Task<List<Class>> GetTop3NearestClassesByStudentId(string studentId)
        {
            return await _context.Classes
                .Join(_context.ClassStudent,
                      c => c.classid,
                      cs => cs.classid,
                      (c, cs) => new { Class = c, ClassStudent = cs })
                .Where(cs => cs.ClassStudent.studentid == studentId) // Lọc theo studentId
                .OrderBy(c => c.Class.datebegin) // Sắp xếp theo ngày bắt đầu tăng dần
                .Select(cs => cs.Class) // Chọn đối tượng lớp học
                .Include(c => c.Teacher) // Bao gồm thông tin giáo viên
                .Include(c => c.Subject) // Bao gồm thông tin môn học
                .Include(c => c.Assignments)
                .Take(3) // Lấy 3 lớp học đầu tiên
                .ToListAsync();
        }

        // Lấy thông tin lớp học theo classId (bao gồm giáo viên và môn học)
        public async Task<Class> GetClassById(string classId)
        {
            return await _context.Classes
                .Include(c => c.Teacher) // Bao gồm thông tin giáo viên
                .Include(c => c.Subject) // Bao gồm thông tin môn học
                .Include(c=> c.Assignments)
                .FirstOrDefaultAsync(c => c.classid == classId); // Lọc theo classId
        }

        // Lấy các lớp học mà học sinh đã đăng ký (bao gồm giáo viên và môn học)
        public async Task<List<Class>> GetClassesByStudentId(string studentId)
        {
            return await _context.Classes
                .Where(c => c.ClassStudents.Any(cs => cs.studentid == studentId)) // Kiểm tra mối quan hệ học sinh-lớp
                .Include(c => c.Teacher) // Bao gồm thông tin giáo viên
                .Include(c => c.Subject) // Bao gồm thông tin môn học
                .Include (c => c.Assignments)
                .ToListAsync();
        }
    }
}
