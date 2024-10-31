using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxAssignmentsViewModel
{
    public class MainWindowBoxAssignmentsViewModel : ViewModelBase
    {
        private MainWindowBoxAssignmentsItem _item; // Private field for backing store

        public ClassViewModel ClassViewModel { get; private set; }
        public AssignmentViewModel AssignmentViewModel { get; private set; }
        private readonly ClassesService _classService;

        private readonly AssignmentService _assignmentService;

        // Public property for data binding
        private MainWindowBoxAssignmentsItem[] _items = new MainWindowBoxAssignmentsItem[3];

        public MainWindowBoxAssignmentsItem this[int index]
        {
            get => _items[index];
            set
            {
                if (_items[index] != value)
                {
                    _items[index] = value;
                    OnPropertyChanged($"Item{index + 1}"); // Notify that the specific item has changed
                }
            }
        }
        public MainWindowBoxAssignmentsItem Item1
        {
            get => this[0];
            set => this[0] = value;
        }

        public MainWindowBoxAssignmentsItem Item2
        {
            get => this[1];
            set => this[1] = value;
        }

        public MainWindowBoxAssignmentsItem Item3
        {
            get => this[2];
            set => this[2] = value;
        }

        public MainWindowBoxAssignmentsViewModel(ClassesService classService, AssignmentService assignmentService)
        {
            _classService = classService;
            _assignmentService = assignmentService;
            ClassViewModel = new ClassViewModel(_classService);
            AssignmentViewModel = new AssignmentViewModel(_assignmentService);
            InitializeData();
        }
        private static (ClassesService, AssignmentService) CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=OOP-new,public;");
            var context = new AppDbContext(optionsBuilder.Options);
            var classesService = new ClassesService(context);
            var assignmentService = new AssignmentService(context);
            return (classesService, assignmentService);

        }
        public MainWindowBoxAssignmentsViewModel() : this(CreateDbContext().Item1, CreateDbContext().Item2)
        {
        }
        private async void InitializeData()
        {
            try
            {
                // Tải 3 lớp gần nhất của sinh viên và hiển thị
                await ClassViewModel.LoadTop3NearestClassesByStudentIdAsync(StudentContext.Instance.StudentId);
                var ClassList = ClassViewModel.Classes.ToList();
                await AssignmentViewModel.LoadNearestAssignmentByClassIDAsync(ClassList[0].classid);
                Item1 = new MainWindowBoxAssignmentsItem(AssignmentViewModel.NearestAssignment.description, AssignmentViewModel.NearestAssignment.duedate.ToString("dd/MM/yyyy"));
                await AssignmentViewModel.LoadNearestAssignmentByClassIDAsync(ClassList[1].classid);
                Item2 = new MainWindowBoxAssignmentsItem(AssignmentViewModel.NearestAssignment.description, AssignmentViewModel.NearestAssignment.duedate.ToString("dd/MM/yyyy"));
                await AssignmentViewModel.LoadNearestAssignmentByClassIDAsync(ClassList[2].classid);
                Item3 = new MainWindowBoxAssignmentsItem(AssignmentViewModel.NearestAssignment.description, AssignmentViewModel.NearestAssignment.duedate.ToString("dd/MM/yyyy"));

            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show($"Có lỗi xảy ra khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }

}
