using System;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
using ClassroomManagementApp1.Commands;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using ClassroomManagementApp1.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxClassesViewModel;

namespace ClassroomManagementApp1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // 1. Create instances of required ViewModels
        public ClassViewModel ClassViewModel { get; private set; }
        public AssignmentViewModel AssignmentViewModel { get; private set; }
        private readonly IClassesService _classService;
        private readonly IAssignmentService _assignmentService;

        // 2. Create instances for RelayCommand
        public ICommand SearchCommand { get; }

        // 3. Create instances of components
        public MainWindowBoxClassesViewModel MainWindowBoxClassesViewModel { get; set; }

        // 4. Variables to store data
        private string _studentId;
        public string StudentId
        {
            get => _studentId;
            set
            {
                _studentId = value;
                OnPropertyChanged(nameof(StudentId));
            }
        }

        private string _classId;
        public string ClassId
        {
            get => _classId;
            set
            {
                _classId = value;
                OnPropertyChanged(nameof(ClassId));
            }
        }

        public ObservableCollection<Class> Classes { get; set; } = new ObservableCollection<Class>();

        // 5. Default constructor, uses CreateDbContext to initialize DbContext and ClassViewModel
        // 6. Constructor to initialize MainWindowViewModel
        public MainWindowViewModel(IClassesService classService, IAssignmentService assignmentService, string studentId)
        {
            _classService = classService;
            _assignmentService = assignmentService;
            ClassViewModel = new ClassViewModel(_classService);
            AssignmentViewModel = new AssignmentViewModel(_assignmentService);
            SearchCommand = new SearchClassCommand(ClassViewModel);
            _studentId = studentId;

            InitializeData();
        }
        private async void InitializeData()
        {
            try
            {
                // Load the top 3 nearest classes for the student and display
                await ClassViewModel.LoadTop3NearestClassesByStudentIdAsync(_studentId);

                // Load assignment data for the first classId (can change classId as needed)
                if (ClassViewModel.Classes.Any())
                {
                    var firstClassId = ClassViewModel.Classes.First().classid; // Get classId of the first class
                    await AssignmentViewModel.LoadAssignmentsByClassIDAsync(firstClassId);
                }
            }
            catch (Exception ex)
            {
                // Handle errors if any
                MessageBox.Show($"Error occurred while loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
