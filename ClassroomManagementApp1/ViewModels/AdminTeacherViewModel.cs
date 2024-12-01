using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Commands;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using ClassroomManagementApp1.Factory;

namespace ClassroomManagementApp1.ViewModels
{
    public class AdminTeacherViewModel : ViewModelBase
    {
        public ICommand AddTeacherCommand { get; }
        public ICommand DeleteTeacherCommand { get; }
        public ICommand SaveCommand { get; }
        private readonly TeacherService _teacherService;
        public TeacherViewModel TeacherViewModel { get; private set; }
        private ObservableCollection<Teacher> _teachers;

        // Collection of teachers for UI binding
        public ObservableCollection<Teacher> Teachers
        {
            get { return _teachers; }
            set
            {
                _teachers = value;
                OnPropertyChanged(nameof(Teachers));
            }
        }

        // Property to store the current view
        private object _selectedView;
        public object SelectedView
        {
            get => _selectedView;
            set
            {
                _selectedView = value;
                OnPropertyChanged(nameof(SelectedView));
            }
        }

        public AdminTeacherViewModel() : this(ServiceFactory.CreateTeacherService())
        {
        }

        // Constructor with dependency injection
        public AdminTeacherViewModel(TeacherService teacherService)
        {
            _teacherService = teacherService;
            TeacherViewModel = new TeacherViewModel(_teacherService);
            AddTeacherCommand = new RelayCommand(_ => AddTemporaryTeacher());
            DeleteTeacherCommand = new RelayCommand(DeleteSelectedTeacher);
            SaveCommand = new RelayCommand(async _ => await SaveChangesToDatabase());
            InitializeData();
        }

        // Initialize data by loading all teachers
        private async void InitializeData()
        {
            Teachers = new ObservableCollection<Teacher>();
            await TeacherViewModel.LoadTeachers();
            var teachersList = TeacherViewModel.Teachers.ToList();
            foreach (var teacher in teachersList)
            {
                Teachers.Add(teacher);
            }
        }

        // Add a temporary teacher if no empty row exists
        private void AddTemporaryTeacher()
        {
            var hasEmptyRow = Teachers.Any(t => string.IsNullOrEmpty(t.teacherid));

            if (!hasEmptyRow) // If no empty row exists, add a new one
            {
                Teachers.Add(new Teacher
                {
                    teacherid = string.Empty,
                    teachername = string.Empty,
                    teacheremail = string.Empty,
                });
            }
            else
            {
                MessageBox.Show("Please fill in the empty row.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Delete the selected teacher
        private void DeleteSelectedTeacher(object parameter)
        {
            if (parameter is Teacher teacher)
            {
                // Confirm deletion
                var result = MessageBox.Show($"Are you sure you want to delete the teacher {teacher.teachername}?",
                                             "Confirmation",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Teachers.Remove(teacher);
                }
            }
            else
            {
                MessageBox.Show("No teacher selected.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Save changes to the database
        private async Task SaveChangesToDatabase()
        {
            try
            {
                // Validate input data
                foreach (var teacher in Teachers)
                {
                    if (string.IsNullOrWhiteSpace(teacher.teacherid) ||
                        string.IsNullOrWhiteSpace(teacher.teachername) ||
                        string.IsNullOrWhiteSpace(teacher.teacheremail))
                    {
                        MessageBox.Show("Please complete all fields for all teachers before saving.",
                                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                // If data is valid, save to the database
                //await _teacherService.UpdateTeachersAsync(Teachers);

                MessageBox.Show("Changes have been saved to the database.",
                                "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
