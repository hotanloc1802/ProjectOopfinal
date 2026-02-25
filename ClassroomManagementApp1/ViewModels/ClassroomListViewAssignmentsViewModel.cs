using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxAssignmentsViewModel;
using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
using ClassroomManagementApp1.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ClassroomManagementApp1.ViewModels
{
    public class ClassroomListViewAssignmentViewModel : ViewModelBase
    {
        private MainWindowBoxAssignmentsItem _item; // Private field for backing store

        public ClassViewModel ClassViewModel { get; private set; }
        public AssignmentViewModel AssignmentViewModel { get; private set; }
        private readonly IClassesService _classService;
        private readonly IAssignmentService _assignmentService;
        public ObservableCollection<Assignment> _listassignment;
        public ObservableCollection<Assignment> ListAssignment
        {
            get => _listassignment;
            set
            {
                _listassignment = value;
                OnPropertyChanged(nameof(ListAssignment));
            }
        }
        // Public property for data binding
        public class AssignmentFormated : Assignment
        {
            public string Date { get; set; }
            public AssignmentFormated(string date, string description)
            {
                this.description = description;
                Date = date;
            }
        }
        public ObservableCollection<AssignmentFormated> AssignmentsFormattedList { get; set; } = new ObservableCollection<AssignmentFormated>();
        public ClassroomListViewAssignmentViewModel(IClassesService classService, IAssignmentService assignmentService)
        {
            _classService = classService;
            _assignmentService = assignmentService;
            ClassViewModel = new ClassViewModel(_classService);
            AssignmentViewModel = new AssignmentViewModel(_assignmentService);
            ListAssignment = new ObservableCollection<Assignment>();
            InitializeData();
        }
        public ClassroomListViewAssignmentViewModel() : this(
            App.Services.GetRequiredService<IClassesService>(),
            App.Services.GetRequiredService<IAssignmentService>())
        {
        }
        private async void InitializeData()
        {
            try
            {
                // Load the 3 nearest classes of the student and display them
                var studentId = App.Services.GetRequiredService<ICurrentStudentContext>().StudentId;
                if (string.IsNullOrWhiteSpace(studentId)) return;

                await ClassViewModel.LoadClassesByStudentIdAsync(studentId);
                ListAssignment.Clear();
                var assignmentsList = ClassViewModel.Assignments;
                foreach (var asm in assignmentsList)
                {
                    var date = asm.duedate.ToString("dd/MM/yyyy");
                    AssignmentsFormattedList.Add(new AssignmentFormated(date, asm.description));
                }
            }
            catch (Exception ex)
            {
                // Handle errors if any
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
