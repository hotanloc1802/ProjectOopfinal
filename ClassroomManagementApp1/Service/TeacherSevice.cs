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

        // Add a teacher
        public async Task AddTeacherAsync(Teacher teacher)
        {
            await _context.Teachers.AddAsync(teacher); // Add teacher to DbSet
            await _context.SaveChangesAsync(); // Save changes to the database
        }

        // Get all teachers
        public async Task<List<Teacher>> GetAllTeachersAsync()
        {
            return await _context.Teachers
                                 .ToListAsync(); // Return a list of teachers
        }

        // Update teacher information
        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            _context.Teachers.Update(teacher); // Update teacher information
            await _context.SaveChangesAsync(); // Save changes to the database
        }

        // Delete a teacher
        public async Task DeleteTeacherAsync(string teacherId)
        {
            var teacher = await _context.Teachers.FindAsync(teacherId); // Find teacher by ID
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher); // Remove teacher
                await _context.SaveChangesAsync(); // Save changes to the database
            }
        }

        // Get teacher information by ID
        public async Task<Teacher> GetTeacherByIdAsync(string teacherId)
        {
            return await _context.Teachers
                                 .FirstOrDefaultAsync(t => t.teacherid == teacherId); // Filter by teacherId
        }
    }
}
