using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using System.Windows;

namespace ClassroomManagementApp1.ClassService
{
    public class StudentService
    {
        // 1. Read DbContext
        private readonly AppDbContext _context;

        // 2. Constructor Service
        public StudentService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // 3. Build Service

        // Thêm học sinh mới
        public async Task AddStudentAsync(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        // Lấy tất cả học sinh
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _context.Students
                .AsNoTracking() // Không theo dõi thực thể, tránh xung đột khi cập nhật
                .ToListAsync();
        }

        // Sửa thông tin của một học sinh
        public async Task UpdateStudentAsync(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            var existingStudent = await _context.Students.FindAsync(student.studentid);
            if (existingStudent != null)
            {
                existingStudent.studentname = student.studentname;
                existingStudent.studentemail = student.studentemail;
                existingStudent.studentgrade = student.studentgrade;
                existingStudent.studentbirth = student.studentbirth;

                _context.Students.Update(existingStudent);
                await _context.SaveChangesAsync();
            }
        }

        // Xóa học sinh
        public async Task DeleteStudentAsync(string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
                throw new ArgumentException("Student ID cannot be null or empty.", nameof(studentId));

            var student = await _context.Students.FindAsync(studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        // Lấy học sinh theo ID
        public async Task<Student> GetStudentById(string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
                throw new ArgumentException("Student ID cannot be null or empty.", nameof(studentId));

            return await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.studentid == studentId);
        }

        // Cập nhật danh sách học sinh
        public async Task UpdateStudentsAsync(IEnumerable<Student> students)
        {
            if (students == null)
                throw new ArgumentNullException(nameof(students));

            foreach (var student in students)
            {
                var existingStudent = await _context.Students.FindAsync(student.studentid);
                if (existingStudent != null)
                {
                    // Cập nhật thông tin
                    existingStudent.studentname = student.studentname;
                    existingStudent.studentemail = student.studentemail;
                    existingStudent.studentgrade = student.studentgrade;
                    existingStudent.studentbirth = student.studentbirth;

                    _context.Students.Update(existingStudent);
                }
                else
                {
                    // Nếu học sinh không tồn tại, thêm mới
                    await _context.Students.AddAsync(student);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
