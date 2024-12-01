using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassroomManagementApp1.ClassService
{
    public class ClassesService
    {
        // 1. Read DbContext

        private readonly AppDbContext _context;

        // 2. Constructor Service

        public ClassesService(AppDbContext context)
        {
            _context = context;
        }

        // 3. Build Service

        // Get all classes for a student by studentId (including teacher and subject information)
        public async Task<List<Class>> GetAllClassesByStudentId(string studentId)
        {
            return await _context.Classes
                .Where(c => c.ClassStudents.Any(cs => cs.studentid == studentId)) // Filter by studentId
                .Include(c => c.Teacher) // Include teacher information
                .Include(c => c.Subject) // Include subject information
                .ToListAsync();
        }

        // Get the top 3 nearest classes by start date (including teacher and subject information)
        public async Task<List<Class>> GetTop3NearestClassesByStudentId(string studentId)
        {
            return await _context.Classes
                .Join(_context.ClassStudent,
                      c => c.classid,
                      cs => cs.classid,
                      (c, cs) => new { Class = c, ClassStudent = cs })
                .Where(cs => cs.ClassStudent.studentid == studentId) // Filter by studentId
                .OrderBy(c => c.Class.datebegin) // Sort by start date in ascending order
                .Select(cs => cs.Class) // Select the class object
                .Include(c => c.Teacher) // Include teacher information
                .Include(c => c.Subject) // Include subject information
                .Include(c => c.Assignments)
                .Take(3) // Take the top 3 classes
                .ToListAsync();
        }

        // Get class information by classId (including teacher and subject information)
        public async Task<Class> GetClassById(string classId)
        {
            return await _context.Classes
                .Include(c => c.Teacher) // Include teacher information
                .Include(c => c.Subject) // Include subject information
                .Include(c => c.Assignments)
                .FirstOrDefaultAsync(c => c.classid == classId); // Filter by classId
        }

        // Get all classes
        public async Task<List<Class>> GetAllClassesAsync()
        {
            return await _context.Classes
                .AsNoTracking() // Do not track entities to avoid update conflicts
                .ToListAsync();
        }

        // Get classes registered by a student (including teacher and subject information)
        public async Task<List<Class>> GetClassesByStudentId(string studentId)
        {
            return await _context.Classes
                .Where(c => c.ClassStudents.Any(cs => cs.studentid == studentId)) // Check student-class relationship
                .Include(c => c.Teacher) // Include teacher information
                .Include(c => c.Subject) // Include subject information
                .Include(c => c.Assignments)
                .ToListAsync();
        }
    }
}
