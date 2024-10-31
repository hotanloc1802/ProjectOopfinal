using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
namespace ClassroomManagementApp1.ClassService
{
    public class TeacherService
    {
        // 1. Read DbContext

        private readonly AppDbContext _context;

        // 2. Constructor Service
        public TeacherService(AppDbContext context)
        {
            _context = context;
        }
        // 3. Build Service

        // Thêm giáo viên
        public async Task AddTeacherAsync(Teacher teacher)
        {
            await _context.Teachers.AddAsync(teacher); // Thêm giáo viên vào DbSet
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
        }

        // Lấy tất cả giáo viên
        public async Task<List<Teacher>> GetAllTeachersAsync()
        {
            return await _context.Teachers
                                 .ToListAsync(); // Trả về danh sách giáo viên
        }

        // Sửa thông tin giáo viên
        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher); // Cập nhật thông tin giáo viên
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
        }

        // Xóa giáo viên
        public async Task DeleteTeacherAsync(string teacherId)
        {
            var teacher = await _context.Teachers.FindAsync(teacherId); // Tìm giáo viên theo ID
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher); // Xóa giáo viên
                await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            }
        }

        // Lấy thông tin giáo viên theo ID
        public async Task<Teacher> GetTeacherByIdAsync(string teacherId)
        {
            return await _context.Teachers
                                 .FirstOrDefaultAsync(t => t.teacherid == teacherId); // Lọc theo teacherId
        }
    }
}
