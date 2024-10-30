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
using System.Windows.Controls;

namespace ClassroomManagementApp1.ViewModels.BoxClasses
{
    public class MainWindowBoxClassesViewModel : ViewModelBase
    {
        private MainWindowBoxClassesItem _item; // Private field for backing store
        private List<Tuple<DateTime, DateTime>> _dateRanges = new List<Tuple<DateTime, DateTime>>();

        public List<Tuple<DateTime, DateTime>> DateRanges
        {
            get => _dateRanges;
            private set
            {
                _dateRanges = value;
                OnPropertyChanged(nameof(DateRanges));
            }
        }

        public ClassViewModel ClassViewModel { get; private set; }
        public AssignmentViewModel AssignmentViewModel { get; private set; }
        private readonly ClassesService _classService;

        private readonly AssignmentService _assignmentService;

        // Public property for data binding
        private MainWindowBoxClassesItem[] _items = new MainWindowBoxClassesItem[3];

        public MainWindowBoxClassesItem this[int index]
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
        public MainWindowBoxClassesItem Item1
        {
            get => this[0];
            set => this[0] = value;
        }

        public MainWindowBoxClassesItem Item2
        {
            get => this[1];
            set => this[1] = value;
        }

        public MainWindowBoxClassesItem Item3
        {
            get => this[2];
            set => this[2] = value;
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
        }//var AssignmentList = ClassViewModel.Assignments.ToList();
        private async void InitializeData()
        {
            try
            {
                // Tải 3 lớp gần nhất của sinh viên và hiển thị
                await ClassViewModel.LoadTop3NearestClassesByStudentIdAsync("S001");
                var ClassList = ClassViewModel.Classes.ToList();
                foreach (var cls in ClassList)
                {
                    SetDateRange(cls.datebegin, cls.dateend);
                }
                await ClassViewModel.LoadAssignmentsByClassIdAsync(ClassList[0].classid);
                Item1 = new MainWindowBoxClassesItem(ClassList[0].Subject.subjectname, ClassList[0].Teacher.teachername, ClassViewModel.Assignments.Count, ClassList[0].datebegin.ToString("dd/MM/yyyy"), ClassList[0].dateend.ToString("dd/MM/yyyy"));
                await ClassViewModel.LoadAssignmentsByClassIdAsync(ClassList[1].classid);
                Item2 = new MainWindowBoxClassesItem(ClassList[1].Subject.subjectname, ClassList[1].Teacher.teachername, ClassViewModel.Assignments.Count, ClassList[1].datebegin.ToString("dd/MM/yyyy"), ClassList[1].dateend.ToString("dd/MM/yyyy"));
                await ClassViewModel.LoadAssignmentsByClassIdAsync(ClassList[2].classid);
                Item3 = new MainWindowBoxClassesItem(ClassList[2].Subject.subjectname, ClassList[2].Teacher.teachername, ClassViewModel.Assignments.Count, ClassList[2].datebegin.ToString("dd/MM/yyyy"), ClassList[2].dateend.ToString("dd/MM/yyyy"));

            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show($"Có lỗi xảy ra khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void SetDateRange(DateTime startDate, DateTime endDate)
        {
            DateRanges.Add(new Tuple<DateTime, DateTime>(startDate, endDate));
            OnPropertyChanged(nameof(DateRanges)); // Notify that DateRanges has changed
        }

    }

}
