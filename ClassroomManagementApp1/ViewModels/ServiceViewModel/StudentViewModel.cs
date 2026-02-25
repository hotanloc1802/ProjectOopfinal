using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClassroomManagementApp1.ViewModels.ServiceViewModels
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        private readonly IStudentService _studentService;
        private ObservableCollection<Student> _students;

        // Collection of students to bind to the UI
        public ObservableCollection<Student> Students
        {
            get => _students;
            set
            {
                _students = value;
                OnPropertyChanged(nameof(Students)); // Notify UI to update
            }
        }

        private Student _selectedStudent;

        // Property for the selected student
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        // Constructor to initialize the StudentService dependency
        public StudentViewModel(IStudentService studentService)
        {
            _studentService = studentService;
            Students = new ObservableCollection<Student>();
        }

        // Load all students asynchronously
        public async Task LoadAllStudentsAsync()
        {
            var studentList = await _studentService.GetAllStudentsAsync();
            Students.Clear();
            foreach (var student in studentList)
            {
                Students.Add(student);
            }
        }

        // Add a new student
        public async Task AddStudentAsync(Student student)
        {
            await _studentService.AddStudentAsync(student);
            await LoadAllStudentsAsync(); // Refresh the list after adding
        }

        // Update an existing student's information
        public async Task UpdateStudentAsync(Student student)
        {
            await _studentService.UpdateStudentAsync(student);
            await LoadAllStudentsAsync(); // Refresh the list after updating
        }

        // Delete a student by their ID
        public async Task DeleteStudentAsync(string studentId)
        {
            await _studentService.DeleteStudentAsync(studentId);
            await LoadAllStudentsAsync(); // Refresh the list after deleting
        }

        // Load a student's information by their ID
        public async Task LoadStudentByIdAsync(string studentId)
        {
            var student = await _studentService.GetStudentById(studentId);
            SelectedStudent = student; // Assign to SelectedStudent for detailed view
        }

        // Event to notify property changes for data binding
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
