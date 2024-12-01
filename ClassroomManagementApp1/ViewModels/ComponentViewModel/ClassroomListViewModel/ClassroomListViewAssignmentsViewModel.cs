using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using ClassroomManagementApp1.Factory;
using ClassroomManagementApp1.DesignPattern;
using System.Collections.ObjectModel;
using System.Windows;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxAssignmentsViewModel;

namespace ClassroomManagementApp1.ViewModels.ComponentViewModel.ClassroomListViewModel
{
    public class ClassroomListViewAssignmentsViewModel : ViewModelBase
    {
        private MainWindowBoxAssignmentsItem _item;

        public AssignmentViewModel AssignmentViewModel { get; private set; }
        private ObservableCollection<Assignment> _listassignments = new ObservableCollection<Assignment>();

        public ObservableCollection<Assignment> Listassignments
        {
            get => _listassignments;
            set
            {
                _listassignments = value;
                OnPropertyChanged(nameof(Listassignments)); // Notify the correct property name
            }
        }

        public MainWindowBoxAssignmentsItem Item
        {
            get => _item;
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item)); // Notify that Item has changed
            }
        }

        // Constructor using Dependency Injection for AssignmentService
        public ClassroomListViewAssignmentsViewModel(AssignmentService assignmentService)
        {
            AssignmentViewModel = new AssignmentViewModel(assignmentService);
            InitializeData();
        }

        // Default constructor using Factory Pattern
        public ClassroomListViewAssignmentsViewModel()
            : this(ServiceFactory.CreateAssignmentService())
        {
        }

        private async void InitializeData()
        {
            try
            {
                // Load assignments for the logged-in student
                await AssignmentViewModel.LoadAssignmentsByStudentId(StudentContextSingleton.Instance.StudentId);

                // Update Listassignments
                Listassignments = new ObservableCollection<Assignment>(AssignmentViewModel.Assignments ?? new ObservableCollection<Assignment>());
            }
            catch (Exception ex)
            {
                // Handle any errors
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
