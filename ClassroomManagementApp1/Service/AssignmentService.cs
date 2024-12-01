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
        // 1. Read DbContext
        private readonly AppDbContext _context;

        // 2. Constructor Service
        public AssignmentService(AppDbContext context)
        {
            _context = context;
        }

        // 3. Build Service

        // Get all assignments
        public async Task<List<Assignment>> GetAllAssignment()
        {
            var assignments = await _context.Assignment
                                 .Include(a => a.Class)  // Include to fetch related Class information
                                 .ToListAsync();

            // Update status for each assignment
            foreach (var assignment in assignments)
            {
                assignment.UpdateStatus();
            }

            // Save changes if status is stored in the database
            await _context.SaveChangesAsync();

            return assignments;
        }

        // Get assignments by ClassID
        public async Task<List<Assignment>> GetAssignmentByClassID(string classId)
        {
            var assignments = await _context.Assignment
                                 .Where(a => a.classid == classId)  // Filter by classId
                                 .OrderBy(a => a.duedate)  // Sort by DueDate in ascending order
                                 .ToListAsync();

            // Update status for each assignment
            foreach (var assignment in assignments)
            {
                assignment.UpdateStatus();
            }

            await _context.SaveChangesAsync(); // Save changes

            return assignments;
        }

        // Get the nearest assignment by ClassID
        public async Task<Assignment> GetNearestAssignmentByClassID(string classId)
        {
            var assignment = await _context.Assignment
                                 .Where(a => a.classid == classId)  // Filter by classId
                                 .OrderBy(a => a.duedate)  // Sort by DueDate in ascending order
                                 .FirstOrDefaultAsync(); // Get the first record (with the nearest DueDate)

            // Update status if assignment is not null
            if (assignment != null)
            {
                assignment.UpdateStatus();
                await _context.SaveChangesAsync(); // Save changes
            }

            return assignment;
        }

        // Get assignments for a student by studentId
        public async Task<List<Assignment>> GetAssignmentsByStudentId(string studentId)
        {
            var assignments = await _context.Assignment
                                 .Where(a => a.Class.ClassStudents.Any(cs => cs.studentid == studentId)) // Filter by studentId
                                 .Include(a => a.Class)
                                 .OrderBy(a => a.duedate) // Sort by DueDate in ascending order
                                 .ToListAsync();

            // Update status for each assignment
            foreach (var assignment in assignments)
            {
                assignment.UpdateStatus();
            }

            await _context.SaveChangesAsync(); // Save changes

            return assignments;
        }
    }
}
