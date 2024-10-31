using System;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
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
        // 1. Tạo các instance của các ViewModel
        public ClassViewModel ClassViewModel { get; private set; }
        public AssignmentViewModel AssignmentViewModel { get; private set; }
        private readonly ClassesService _classService;
        private readonly AssignmentService _assignmentService;
        // 2. Tạo các instance phương thức Relaycommand
        public ICommand SearchCommand { get; }
        // 3. Tạo các instance của component
        public MainWindowBoxClassesViewModel MainWindowBoxClassesViewModel { get; set; }
        // 4. Tạo các biến lưu trữ
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
        private string _classtId;
        public string ClassId
        {
            get => _classtId;
            set
            {
                _classtId = value;
                OnPropertyChanged(nameof(ClassId));
            }
        }
        public ObservableCollection<Class> Classes { get; set; } = new ObservableCollection<Class>();

        //5. Constructor mặc định, sử dụng phương thức CreateDbContext để tạo DbContext và khởi tạo ClassViewModel
        public MainWindowViewModel(string studentid) : this(CreateDbContext().Item1, CreateDbContext().Item2, studentid )
        {
        }
        // 6. Khởi tạo MainWindowViewModel
        public MainWindowViewModel(ClassesService classService, AssignmentService assignmentService,string studentid)
        {
            _classService = classService;
            _assignmentService = assignmentService;
            ClassViewModel = new ClassViewModel(_classService);
            AssignmentViewModel = new AssignmentViewModel(_assignmentService);
            SearchCommand = new SearchClassCommand(ClassViewModel);
            _studentId = studentid;

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
        private async void InitializeData()
        {
            try
            {
                // Tải 3 lớp gần nhất của sinh viên và hiển thị
                await ClassViewModel.LoadTop3NearestClassesByStudentIdAsync("S001");

                // Tải dữ liệu assignment theo classId (có thể thay đổi classId theo ý bạn)
                if (ClassViewModel.Classes.Any())
                {
                    var firstClassId = ClassViewModel.Classes.First().classid; // Lấy classId của lớp đầu tiên
                    await AssignmentViewModel.LoadAssignmentsByClassIDAsync(firstClassId);
                }

            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show($"Có lỗi xảy ra khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
