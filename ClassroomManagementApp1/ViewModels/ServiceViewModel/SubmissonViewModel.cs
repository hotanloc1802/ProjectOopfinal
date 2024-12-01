using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ClassroomManagementApp1.ViewModels.ServiceViewModels
{
    public class SubmissionViewModel : INotifyPropertyChanged
    {
        private readonly SubmissionService _submissionService;

        // Collection of submissions to notify UI of changes
        public ObservableCollection<Submission> Submissions { get; set; } = new ObservableCollection<Submission>();

        private Submission _selectedSubmission;

        // Property for the selected submission
        public Submission SelectedSubmission
        {
            get => _selectedSubmission;
            set
            {
                _selectedSubmission = value;
                OnPropertyChanged(nameof(SelectedSubmission));
            }
        }

        // Constructor to initialize the SubmissionService dependency
        public SubmissionViewModel(SubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        // Load all submissions from the service
        private async void LoadSubmissions()
        {
            var submissionsList = await _submissionService.GetAllSubmissionsAsync();
            UpdateSubmissions(submissionsList);
        }

        // Update the Submissions collection with new data
        private void UpdateSubmissions(IEnumerable<Submission> submissions)
        {
            Submissions.Clear();
            foreach (var submission in submissions)
            {
                Submissions.Add(submission);
            }
        }

        // Add a new submission
        public async Task AddSubmission(Submission submission)
        {
            await _submissionService.AddSubmissionAsync(submission);
            LoadSubmissions();
        }

        // Update the currently selected submission
        public async Task UpdateSubmission()
        {
            if (SelectedSubmission != null)
            {
                await _submissionService.UpdateSubmissionAsync(SelectedSubmission);
                LoadSubmissions();
            }
        }

        // Delete the currently selected submission
        public async Task DeleteSubmission()
        {
            if (SelectedSubmission != null)
            {
                await _submissionService.DeleteSubmissionAsync(SelectedSubmission.submissionid);
                Submissions.Remove(SelectedSubmission);
                SelectedSubmission = null;
                LoadSubmissions();
            }
        }

        // Load submissions by student ID
        public async Task LoadSubmissionsByStudentId(string studentId)
        {
            try
            {
                if (string.IsNullOrEmpty(studentId))
                {
                    throw new ArgumentException("Student ID cannot be null or empty", nameof(studentId));
                }

                var submissionList = await _submissionService.GetSubmissionsByStudentId(studentId) ?? new List<Submission>();

                Submissions.Clear(); // Clear the old items before adding new ones

                foreach (var submission in submissionList)
                {
                    Submissions.Add(submission);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading submissions: {ex.Message}");
                throw; // Re-throw for higher-level error handling if necessary
            }
        }

        // Load submission by student ID and assignment ID
        public async Task LoadSubmissionsByStudentIdAndAssignmentId(string studentId, string assignmentId)
        {
            try
            {
                SelectedSubmission = await _submissionService.GetSubmissionsByStudentIdAndAssignmentId(studentId, assignmentId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading submissions by student ID and assignment ID: {ex.Message}");
                SelectedSubmission = null; // Handle null if necessary
            }
        }

        // Event to notify UI of property changes
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
