using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ClassroomManagementApp1.ViewModels.ServiceViewModels
{
    public class TeacherViewModel : INotifyPropertyChanged
    {
        private readonly ITeacherService _teacherService;

        // Collection of teachers to notify UI of changes
        public ObservableCollection<Teacher> Teachers { get; set; }

        // Property to display the currently selected teacher
        private Teacher _selectedTeacher;
        public Teacher SelectedTeacher
        {
            get => _selectedTeacher;
            set
            {
                _selectedTeacher = value;
                OnPropertyChanged(nameof(SelectedTeacher));
            }
        }

        // Constructor to initialize the TeacherService dependency
        public TeacherViewModel(ITeacherService teacherService)
        {
            _teacherService = teacherService;
            Teachers = new ObservableCollection<Teacher>();
        }

        // Load all teachers asynchronously
        public async Task LoadTeachers()
        {
            var teacherList = await _teacherService.GetAllTeachersAsync();
            Teachers.Clear();
            foreach (var teacher in teacherList)
            {
                Teachers.Add(teacher);
            }
        }

        // Add a new teacher
        public async Task AddTeacher(Teacher teacher)
        {
            await _teacherService.AddTeacherAsync(teacher);
        }

        // Update the currently selected teacher's information
        public async Task UpdateTeacher()
        {
            if (SelectedTeacher != null)
            {
                await _teacherService.UpdateTeacherAsync(SelectedTeacher);
            }
        }

        // Delete the currently selected teacher
        public async Task DeleteTeacher()
        {
            if (SelectedTeacher != null)
            {
                await _teacherService.DeleteTeacherAsync(SelectedTeacher.teacherid);
                Teachers.Remove(SelectedTeacher);
                SelectedTeacher = null;
            }
        }

        // Get information of a teacher by their ID
        public async Task<string> GetTeacherInfo(string teacherid)
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            var teacher = teachers.FirstOrDefault(s => s.teacherid == teacherid);

            if (teacher != null)
            {
                return $"Teacher ID: {teacher.teacherid}";
            }
            return "Teacher not found.";
        }

        // Event to notify UI of property changes
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
