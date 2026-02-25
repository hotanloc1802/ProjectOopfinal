using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ClassroomManagementApp1.ViewModels.ServiceViewModels
{
    public class ClassViewModel : INotifyPropertyChanged
    {
        private readonly IClassesService _classService;

        // ObservableCollection to notify UI of changes
        public ObservableCollection<Class> Classes { get; set; } = new ObservableCollection<Class>();
        public ObservableCollection<Assignment> Assignments { get; set; } = new ObservableCollection<Assignment>();
        public ObservableCollection<HashSet<Tuple<string, string>>> StudentSubmisson { get; set; } = new ObservableCollection<HashSet<Tuple<string, string>>>();

        private Class _selectedClass; // Property for the selected class
        public Class SelectedClass
        {
            get => _selectedClass;
            set
            {
                _selectedClass = value;
                OnPropertyChanged(nameof(SelectedClass)); // Notify UI of the change
            }
        }

        // Constructor to initialize the ClassesService dependency
        public ClassViewModel(IClassesService classService)
        {
            _classService = classService;
        }

        // Load all classes for a student
        public async Task LoadAllClassesAsync(string studentId)
        {
            var classList = await _classService.GetAllClassesByStudentId(studentId);
            Classes.Clear();
            foreach (var cls in classList)
            {
                Classes.Add(cls);
            }
        }

        // Load all classes (without filtering by student)
        public async Task LoadAllClassAsync()
        {
            var classesList = await _classService.GetAllClassesAsync();
            Classes.Clear();
            foreach (var classe in classesList)
            {
                Classes.Add(classe);
            }
        }

        // Load the top 3 nearest classes for a student by student ID
        public async Task LoadTop3NearestClassesByStudentIdAsync(string studentId)
        {
            var classList = await _classService.GetTop3NearestClassesByStudentId(studentId);
            Classes.Clear();
            foreach (var cls in classList)
            {
                Classes.Add(cls);
            }
        }

        // Load information for a specific class by ClassID
        public async Task LoadClassByIdAsync(string classId)
        {
            var cls = await _classService.GetClassById(classId);
            if (cls != null) // Check if the class exists
            {
                SelectedClass = cls; // Set the selected class
                Assignments.Clear(); // Clear old assignments
                foreach (var assignment in cls.Assignments)
                {
                    // Update the status of the assignment
                    assignment.UpdateStatus();
                    // Add the assignment only if it's still valid
                    if (assignment.status == 1) // 1 means the assignment is still valid
                    {
                        Assignments.Add(assignment);
                    }
                }
            }
            else
            {
                // Handle cases where the class does not exist
                SelectedClass = null;
            }
        }

        // Load all classes and assignments for a student
        public async Task LoadClassesByStudentIdAsync(string studentId)
        {
            var classList = await _classService.GetClassesByStudentId(studentId);
            Classes.Clear();
            foreach (var cls in classList)
            {
                Classes.Add(cls);
                foreach (var assignment in cls.Assignments)
                {
                    Assignments.Add(assignment);
                }
            }
        }

        // Load assignments for a specific class by ClassID
        public async Task LoadAssignmentsByClassIdAsync(string classId)
        {
            var cls = await _classService.GetClassById(classId);

            if (cls != null) // Check if the class exists
            {
                Classes.Clear();
                SelectedClass = cls;
                Assignments.Clear(); // Clear old assignments

                foreach (var assignment in cls.Assignments)
                {
                    // Update the status of the assignment
                    assignment.UpdateStatus();
                    // Add the assignment only if it's still valid
                    if (assignment.status == 1) // 1 means the assignment is still valid
                    {
                        Assignments.Add(assignment);
                    }
                }
            }
        }

        // PropertyChanged event handler for UI updates
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
