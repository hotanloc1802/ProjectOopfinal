using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using System.Security.Principal;

namespace ClassroomManagementApp1.ClassService
{
    public class StudentService
    {
        // 1. Read DbContext
        private readonly AppDbContext _context;

        // 2. Constructor Service
        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        // 3. Build Service

        // Thêm học sinh
        public async Task AddStudentAsync(Student student)
        {
            // Thêm học sinh vào DbSet
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
        }

        // Lấy tất cả học sinh
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync(); // Lấy tất cả học sinh
        }

        // Sửa thông tin học sinh
        public async Task UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student); // Cập nhật học sinh
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
        }

        // Xóa học sinh
        public async Task DeleteStudentAsync(string studentId)
        {
            var student = await _context.Students.FindAsync(studentId); // Tìm học sinh theo studentId
            if (student != null)
            {
                _context.Students.Remove(student); // Xóa học sinh
                await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            }
        }

        // Lấy thông tin học sinh theo ID
        public async Task<Student> GetStudentById(string studentId)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.studentid == studentId); // Lấy học sinh theo mã sinh viên
        }
       /* public async Task<bool> UpdateAccountAsync(string studentId, StudentViewModel studentViewModel)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.studentid == studentId);

            if (student == null)
            {
                return false;
            }

            // Update the fields of the student entity from the ViewModel
            student.studentname = studentViewModel.StudentName;
            student.studentbirth = DateTime.ParseExact(studentViewModel.StudentBirth, "dd/MM/yyyy", null);

            try
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
                return true;  // Indicate success
            }
            catch (Exception ex)
            {
                // Handle exception if needed
                Console.WriteLine(ex.Message);
                return false;  // Indicate failure
            }
        }
        */


    }
}
