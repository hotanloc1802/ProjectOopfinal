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
    public class SubmissionService
    {
        // 1. Read DbContext

        private readonly AppDbContext _context;

        // 2. Constructor Service
        public SubmissionService(AppDbContext context)
        {
            _context = context;
        }

        // 3. Build Service

        // Add a submission
        public async Task AddSubmissionAsync(Submission submission)
        {
            if (submission == null)
            {
                throw new ArgumentNullException(nameof(submission), "Submission cannot be null");
            }

            await _context.Submissions.AddAsync(submission);
            await _context.SaveChangesAsync();
        }

        // Get all submissions
        public async Task<List<Submission>> GetAllSubmissionsAsync()
        {
            return await _context.Submissions.ToListAsync();
        }

        // Update a submission
        public async Task UpdateSubmissionAsync(Submission submission)
        {
            if (submission == null)
            {
                throw new ArgumentNullException(nameof(submission), "Submission cannot be null");
            }

            _context.Submissions.Update(submission);
            await _context.SaveChangesAsync();
        }

        // Delete a submission
        public async Task DeleteSubmissionAsync(string submissionID)
        {
            if (string.IsNullOrEmpty(submissionID))
            {
                throw new ArgumentException("Submission ID cannot be null or empty", nameof(submissionID));
            }

            var submission = await _context.Submissions.FindAsync(submissionID);
            if (submission != null)
            {
                _context.Submissions.Remove(submission);
                await _context.SaveChangesAsync();
            }
        }

        // Get submissions by student ID
        public async Task<List<Submission>> GetSubmissionsByStudentId(string studentId)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                throw new ArgumentException("Student ID cannot be null or empty", nameof(studentId));
            }

            try
            {
                return await _context.Submissions
                    .Where(s => s.studentid == studentId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching submissions for student ID {studentId}: {ex.Message}");
                throw; // Rethrow exception for higher-level handling if necessary
            }
        }

        // Get submission by student ID and assignment ID
        public async Task<Submission> GetSubmissionsByStudentIdAndAssignmentId(string studentId, string assignmentId)
        {
            try
            {
                return await _context.Submissions
                    .Where(s => s.studentid == studentId && s.assignmentid == assignmentId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // Log or display error message
                Console.WriteLine($"An error occurred while querying: {ex.Message}");
                return null;
            }
        }
    }
}
