using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using ClassroomManagementApp1.DesignPattern;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using ClassroomManagementApp1.Component;
using ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxAssignmentsViewModel;
using ClassroomManagementApp1.Factory;

namespace ClassroomManagementApp1.ViewModels
{
    public class ClassroomListViewAssignmentViewModel : ViewModelBase
    {
        private MainWindowBoxAssignmentsItem _item; // Private field for backing store

        public ClassViewModel ClassViewModel { get; private set; }
        public AssignmentViewModel AssignmentViewModel { get; private set; }
        private readonly ClassesService _classService;

        private readonly AssignmentService _assignmentService;
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
        public ClassroomListViewAssignmentViewModel(ClassesService classService, AssignmentService assignmentService)
        {
            _classService = classService;
            _assignmentService = assignmentService;
            ClassViewModel = new ClassViewModel(_classService);
            AssignmentViewModel = new AssignmentViewModel(_assignmentService);
            ListAssignment = new ObservableCollection<Assignment>();
            InitializeData();
        }
        public ClassroomListViewAssignmentViewModel() : this(ServiceFactory.CreateClassesService(), ServiceFactory.CreateAssignmentService())
        {
        }
        private async void InitializeData()
        {
            try
            {
                // Load the 3 nearest classes of the student and display them
                await ClassViewModel.LoadClassesByStudentIdAsync(StudentContextSingleton.Instance.StudentId);
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
