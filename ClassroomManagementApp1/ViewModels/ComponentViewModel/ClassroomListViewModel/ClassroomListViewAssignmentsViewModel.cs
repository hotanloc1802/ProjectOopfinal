using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxAssignmentsViewModel;
using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
using ClassroomManagementApp1.Services;
using Microsoft.Extensions.DependencyInjection;

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
        public ClassroomListViewAssignmentsViewModel(IAssignmentService assignmentService)
        {
            AssignmentViewModel = new AssignmentViewModel(assignmentService);
            InitializeData();
        }

        public ClassroomListViewAssignmentsViewModel()
            : this(App.Services.GetRequiredService<IAssignmentService>())
        {
        }

        private async void InitializeData()
        {
            try
            {
                // Load assignments for the logged-in student
                var studentId = App.Services.GetRequiredService<ICurrentStudentContext>().StudentId;
                if (string.IsNullOrWhiteSpace(studentId)) return;

                await AssignmentViewModel.LoadAssignmentsByStudentId(studentId);

                // Update Listassignments
                Listassignments = new ObservableCollection<Assignment>(AssignmentViewModel.Assignments);
            }
            catch (Exception ex)
            {
                // Handle any errors
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
