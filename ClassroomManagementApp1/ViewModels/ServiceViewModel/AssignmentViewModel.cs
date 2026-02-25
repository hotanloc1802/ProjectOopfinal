using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ClassroomManagementApp1.ViewModels.ServiceViewModels
{
    public class AssignmentViewModel
    {
        private readonly IAssignmentService _assignmentService;

        // ObservableCollection to notify UI of changes
        public ObservableCollection<Assignment> Assignments { get; set; } = new ObservableCollection<Assignment>();

        private Assignment _nearestAssignment;

        // Property to store the nearest assignment
        public Assignment NearestAssignment
        {
            get => _nearestAssignment;
            set
            {
                _nearestAssignment = value;
            }
        }

        // Constructor to initialize AssignmentService
        public AssignmentViewModel(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService; // Initialize the assignment service
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Load all assignments asynchronously
        public async Task LoadAllAssignmentsAsync()
        {
            var assignmentList = await _assignmentService.GetAllAssignment();
            Assignments.Clear();
            foreach (var asm in assignmentList)
            {
                Assignments.Add(asm);
            }
        }

        // Load assignments by class ID asynchronously
        public async Task LoadAssignmentsByClassIDAsync(string classID)
        {
            var assignmentList = await _assignmentService.GetAssignmentByClassID(classID);
            Assignments.Clear();
            foreach (var asm in assignmentList)
            {
                Assignments.Add(asm);
            }
        }

        // Load the nearest assignment by class ID asynchronously
        public async Task LoadNearestAssignmentByClassIDAsync(string classID)
        {
            var nearest = await _assignmentService.GetNearestAssignmentByClassID(classID);
            NearestAssignment = nearest;
        }

        // Load assignments for a specific student by their ID
        public async Task LoadAssignmentsByStudentId(string studentId)
        {
            if (!string.IsNullOrEmpty(studentId))
            {
                var assignments = await _assignmentService.GetAssignmentsByStudentId(studentId);
                Assignments.Clear();
                foreach (var assignment in assignments)
                {
                    Assignments.Add(assignment);
                }
            }
        }
    }
}
