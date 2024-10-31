using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
namespace ClassroomManagementApp1.ViewModels.ServiceViewModels
{
    public class SubmissionViewModel : INotifyPropertyChanged
    {
        private readonly SubmissionService _submissionService;

        public ObservableCollection<Submission> Submissions { get; set; } = new ObservableCollection<Submission>();
        private Submission _selectedSubmission;
        public Submission SelectedSubmission
        {
            get => _selectedSubmission;
            set
            {
                _selectedSubmission = value;
                OnPropertyChanged(nameof(SelectedSubmission));
            }
        }

        public SubmissionViewModel (SubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        private async void LoadSubmissions()
        {
            var submissionsList = await _submissionService.GetAllSubmissionsAsync();
            UpdateSubmissions(submissionsList);
        }

        private void UpdateSubmissions(IEnumerable<Submission> submissions)
        {
            Submissions.Clear();
            foreach (var submission in submissions)
            {
                Submissions.Add(submission);
            }
        }

        public async Task AddSubmission(Submission submission)
        {
            await _submissionService.AddSubmissionAsync(submission);
            LoadSubmissions();
        }

        public async Task UpdateSubmission()
        {
            if (SelectedSubmission != null)
            {
                await _submissionService.UpdateSubmissionAsync(SelectedSubmission);
                LoadSubmissions();
            }
        }

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

        public async Task LoadSubmissionsByStudentId(string studentId)
        {
            try
            {
                if (string.IsNullOrEmpty(studentId))
                {
                    throw new ArgumentException("Student ID cannot be null or empty", nameof(studentId));
                }

                var submissionList = await _submissionService.GetSubmissionsByStudentId(studentId) ?? new List<Submission>();

                Submissions.Clear(); // Xóa các phần tử cũ trước khi thêm mới

                foreach (var submission in submissionList)
                {
                    Submissions.Add(submission);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading submissions: {ex.Message}");
                throw; // Giữ lại để xử lý lỗi ở mức cao hơn nếu cần
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
