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

        // Add a new student
        public async Task AddStudentAsync(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        // Get all students
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _context.Students
                .AsNoTracking() // Do not track entities to avoid update conflicts
                .ToListAsync();
        }

        // Update student information
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

        // Delete a student
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

        // Get student by ID
        public async Task<Student> GetStudentById(string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
                throw new ArgumentException("Student ID cannot be null or empty.", nameof(studentId));

            return await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.studentid == studentId);
        }

        // Update a list of students
        public async Task UpdateStudentsAsync(IEnumerable<Student> students)
        {
            if (students == null)
                throw new ArgumentNullException(nameof(students));

            foreach (var student in students)
            {
                var existingStudent = await _context.Students.FindAsync(student.studentid);
                if (existingStudent != null)
                {
                    // Update information
                    existingStudent.studentname = student.studentname;
                    existingStudent.studentemail = student.studentemail;
                    existingStudent.studentgrade = student.studentgrade;
                    existingStudent.studentbirth = student.studentbirth;

                    _context.Students.Update(existingStudent);
                }
                else
                {
                    // If the student does not exist, add it
                    await _context.Students.AddAsync(student);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
