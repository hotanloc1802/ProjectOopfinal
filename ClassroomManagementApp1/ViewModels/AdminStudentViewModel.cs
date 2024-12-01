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
    public class AdminStudentViewModel : ViewModelBase
    {
        public ICommand AddStudentCommand { get; }
        public ICommand DeleteStudentCommand { get; }
        public ICommand SaveCommand { get; }
        private readonly StudentService _studentService;
        public StudentViewModel StudentViewModel { get; private set; }
        private ObservableCollection<Student> _students;

        // Collection of students for UI binding
        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged(nameof(Students));
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
                OnPropertyChanged(nameof(SelectedView)); // Notify UI about changes
            }
        }

       
        // Constructor with default context creation
        public AdminStudentViewModel() : this(ServiceFactory.CreateStudentService())
        {
        }

        // Constructor with dependency injection
        public AdminStudentViewModel(StudentService studentService)
        {
            _studentService = studentService;
            StudentViewModel = new StudentViewModel(_studentService);
            AddStudentCommand = new RelayCommand(_ => AddTemporaryStudent());
            DeleteStudentCommand = new RelayCommand(DeleteSelectedStudent);
            SaveCommand = new RelayCommand(async _ => await SaveChangesToDatabase());
            InitializeData();
        }

        // Initialize data by loading all students
        private async void InitializeData()
        {
            Students = new ObservableCollection<Student>();
            await StudentViewModel.LoadAllStudentsAsync();
            var studentslist = StudentViewModel.Students.ToList();
            foreach (var student in studentslist)
            {
                Students.Add(student);
            }
        }

        // Add a temporary student if no empty row exists
        private void AddTemporaryStudent()
        {
            var hasEmptyRow = Students.Any(s => string.IsNullOrEmpty(s.studentid));

            if (!hasEmptyRow) // If no empty row exists, add a new one
            {
                Students.Add(new Student
                {
                    studentid = string.Empty,    // Empty ID
                    studentname = string.Empty,  // Empty Name
                    studentemail = string.Empty, // Empty Email
                    studentgrade = string.Empty, // Default Value
                    studentbirth = string.Empty  // Empty Date of Birth
                });
            }
            else
            {
                MessageBox.Show("Please fill in the empty row.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Delete the selected student
        private void DeleteSelectedStudent(object parameter)
        {
            if (parameter is Student student)
            {
                // Confirm deletion
                var result = MessageBox.Show($"Are you sure you want to delete the student {student.studentname}?",
                                             "Confirmation",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Students.Remove(student);
                }
            }
            else
            {
                MessageBox.Show("No student selected.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Save changes to the database
        private async Task SaveChangesToDatabase()
        {
            try
            {
                // Validate input data
                foreach (var student in Students)
                {
                    if (string.IsNullOrWhiteSpace(student.studentid) ||
                        string.IsNullOrWhiteSpace(student.studentname) ||
                        string.IsNullOrWhiteSpace(student.studentemail) ||
                        string.IsNullOrWhiteSpace(student.studentgrade) ||
                        string.IsNullOrWhiteSpace(student.studentbirth))
                    {
                        MessageBox.Show("Please complete all fields for all students before saving.",
                                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                // If data is valid, save to the database
                await _studentService.UpdateStudentsAsync(Students);

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
