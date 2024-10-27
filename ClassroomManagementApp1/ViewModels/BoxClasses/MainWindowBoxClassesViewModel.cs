using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassroomManagementApp1.ViewModels.BoxClasses
{
    public class MainWindowBoxClassesViewModel : ViewModelBase
    {
        private MainWindowBoxClassesItem _item; // Private field for backing store

        public ClassViewModel ClassViewModel { get; private set; }
        public AssignmentViewModel AssignmentViewModel { get; private set; }
        private readonly ClassesService _classService;

        private readonly AssignmentService _assignmentService;

        // Public property for data binding
        public MainWindowBoxClassesItem Item
        {
            get => _item;
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item)); // Notify that Item has changed
            }
        }
        public MainWindowBoxClassesViewModel(ClassesService classService, AssignmentService assignmentService)
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
        public MainWindowBoxClassesViewModel() : this(CreateDbContext().Item1, CreateDbContext().Item2)
        {
        }
        private async void InitializeData()
        {
            try
            {
                // Tải 3 lớp gần nhất của sinh viên và hiển thị
                await ClassViewModel.LoadTop3NearestClassesByStudentIdAsync("S001");
                var ClassList = ClassViewModel.Classes.ToList();
                var dateBegin = ClassList[0].datebegin.ToString("dd/MM/yyyy");
                var dateEnd = ClassList[0].dateend.ToString("dd/MM/yyyy");
                Item = new MainWindowBoxClassesItem(ClassList[0].Subject.subjectname, ClassList[0].Teacher.teacherid, dateBegin , dateEnd);

            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show($"Có lỗi xảy ra khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }

}
