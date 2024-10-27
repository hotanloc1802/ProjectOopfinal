using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassroomManagementApp1.ViewModels.BoxClasses
{
    public class ClassroomListViewAssignmentsViewModel : ViewModelBase
    {
        private MainWindowBoxAssignmentsItem _item; // Private field for backing store

        public AssignmentViewModel AssignmentViewModel { get; private set; }
        private ObservableCollection<Assignment> _listassignments = new ObservableCollection<Assignment>();
        public ObservableCollection<Assignment> Listassignments
        {
            get => _listassignments;
            set
            {
                _listassignments = value;
                OnPropertyChanged(nameof(Listassignments)); // Thông báo đúng tên thuộc tính
            }
        }

        private readonly AssignmentService _assignmentService;

        // Public property for data binding
        public MainWindowBoxAssignmentsItem Item
        {
            get => _item;
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item)); // Notify that Item has changed
            }
        }
        public ClassroomListViewAssignmentsViewModel(AssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
            AssignmentViewModel = new AssignmentViewModel(_assignmentService);
            InitializeData();
        }

        // Method to create and return ClassesService instance
        private static AssignmentService CreateAssignmenService()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=OOP-new,public;");
            var context = new AppDbContext(optionsBuilder.Options);
            return new AssignmentService(context);
        }

        // Default constructor that uses CreateClassService
        public ClassroomListViewAssignmentsViewModel() : this(CreateAssignmenService())
        {
        }
        private async void InitializeData()
        {
            try
            {
                await AssignmentViewModel.LoadAssignmentsByStudentId("S002");
                Listassignments = new ObservableCollection<Assignment>(AssignmentViewModel.Assignments);


            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show($"Có lỗi xảy ra khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }

}
