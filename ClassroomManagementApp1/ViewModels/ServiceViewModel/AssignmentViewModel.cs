using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomManagementApp1.ViewModels.ServiceViewModels
{
    public class AssignmentViewModel
    {
        private readonly AssignmentService _assignmentService;

        // ObservableCollection to notify UI of changes
        public ObservableCollection<Assignment> Assignments { get; set; } = new ObservableCollection<Assignment>();
        private Assignment _nearestAssignment;
        public Assignment NearestAssignment
        {
            get => _nearestAssignment;
            set
            {
                _nearestAssignment = value;
            }
        }
        public AssignmentViewModel(AssignmentService assignmentService)
        {
            _assignmentService = assignmentService;// Initialize the class service
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async Task LoadAllAssignmentsAsync()
        {
            var assignmentList = await _assignmentService.GetAllAssignment();
            Assignments.Clear();
            foreach (var asm in assignmentList)
            {
                Assignments.Add(asm);
            }
        }
        public async Task LoadAssignmentsByClassIDAsync(string classID)
        {
            var assignmentList = await _assignmentService.GetAssignmentByClassID(classID);
            Assignments.Clear();
            foreach (var asm in assignmentList)
            {
                Assignments.Add(asm);
            }
        }
        public async Task LoadNearestAssignmentByClassIDAsync(string classID)
        {
            var nearest = await _assignmentService.GetNearestAssignmentByClassID(classID);
            NearestAssignment = nearest;
        }
        public async Task LoadAssignmentsByStudentId(string studentid)
        {
            if (!string.IsNullOrEmpty(studentid))
            {
                var assignments = await _assignmentService.GetAssignmentsByStudentId(studentid);
                Assignments.Clear();
                foreach (var assignment in assignments)
                {
                    Assignments.Add(assignment);
                }
            }
        }
    }
}
