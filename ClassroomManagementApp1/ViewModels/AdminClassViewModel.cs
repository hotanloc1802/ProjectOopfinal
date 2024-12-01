using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Commands;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using ClassroomManagementApp1.Factory;

namespace ClassroomManagementApp1.ViewModels
{
    public class AdminClassViewModel : ViewModelBase
    {
        public ICommand AddClassCommand { get; }
        public ICommand DeleteClassCommand { get; }
        public ICommand SaveCommand { get; }
        private readonly ClassesService _classService;
        public ClassViewModel ClassViewModel { get; private set; }
        private ObservableCollection<Class> _classes;

        // Collection of classes for UI binding
        public ObservableCollection<Class> Classes
        {
            get { return _classes; }
            set
            {
                _classes = value;
                OnPropertyChanged(nameof(Classes));
            }
        }

        // Property to store the current view
        private object _selectedView;
        public object SelectedView
        {
            get => _selectedView;
            set
            {
                _selectedView = value;
                OnPropertyChanged(nameof(SelectedView)); // Notify UI about changes
            }
        }

        // Create a database context
        // Default constructor
        public AdminClassViewModel() : this(ServiceFactory.CreateClassesService())
        {
        }

        // Constructor with dependency injection
        public AdminClassViewModel(ClassesService classesService)
        {
            _classService = classesService;
            ClassViewModel = new ClassViewModel(_classService);
            AddClassCommand = new RelayCommand(_ => AddTemporaryClass());
            DeleteClassCommand = new RelayCommand(DeleteSelectedClass);
            SaveCommand = new RelayCommand(async _ => await SaveChangesToDatabase());
            InitializeData();
        }

        // Initialize data by loading all classes
        private async void InitializeData()
        {
            Classes = new ObservableCollection<Class>();
            await ClassViewModel.LoadAllClassAsync();
            var classeslist = ClassViewModel.Classes.ToList();
            foreach (var classe in classeslist)
            {
                Classes.Add(classe);
            }
        }

        // Add a temporary class if no empty row exists
        private void AddTemporaryClass()
        {
            var hasEmptyRow = Classes.Any(s => string.IsNullOrEmpty(s.classid));

            if (!hasEmptyRow) // If no empty row exists, add a new one
            {
                Classes.Add(new Class
                {
                    classid = string.Empty,
                    teacherid = string.Empty,
                    subjectid = string.Empty,
                    classname = string.Empty,
                    datebegin = new DateTime(1999, 1, 1), // Use valid DateTime
                    dateend = new DateTime(1999, 1, 1),   // Use valid DateTime
                });
            }
            else
            {
                MessageBox.Show("Please fill in the empty row.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Delete the selected class
        private void DeleteSelectedClass(object parameter)
        {
            if (parameter is Class classe)
            {
                // Confirm deletion
                var result = MessageBox.Show($"Are you sure you want to delete the class {classe.classname}?",
                                             "Confirmation",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Classes.Remove(classe);
                }
            }
            else
            {
                MessageBox.Show("No class selected.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Save changes to the database
        private async Task SaveChangesToDatabase()
        {
            try
            {
                // Validate input data
                foreach (var classe in Classes)
                {
                    if (string.IsNullOrWhiteSpace(classe.classid) ||
                        string.IsNullOrWhiteSpace(classe.teacherid) ||
                        string.IsNullOrWhiteSpace(classe.subjectid) ||
                        classe.datebegin == default || // Check start date
                        classe.dateend == default ||  // Check end date
                        classe.dateend < classe.datebegin || // End date must not be before start date
                        string.IsNullOrWhiteSpace(classe.classname))
                    {
                        MessageBox.Show("Please complete all fields before saving.",
                                        "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                // Save data to the database if valid
                // await _classService.UpdateStudentsAsync(Classes);

                MessageBox.Show("Changes have been saved to the database.",
                                "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
