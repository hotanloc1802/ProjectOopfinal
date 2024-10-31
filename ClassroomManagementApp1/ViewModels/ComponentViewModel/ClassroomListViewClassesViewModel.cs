using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxClassesViewModel;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ClassroomManagementApp1.ViewModels.ComponentViewModel
{
    public class ClassroomListViewClassesViewModel : ViewModelBase
    {
        private MainWindowBoxClassesItem _item; // Private field for backing store
        public ClassViewModel ClassViewModel { get; private set; }
        private readonly ClassesService _classService;
        private List<Tuple<DateTime, DateTime>> _dateRanges = new List<Tuple<DateTime, DateTime>>();
        public ObservableCollection<ClassWithDateRange> _listclasswithdaterange { get; set; }
        public ObservableCollection<ClassWithDateRange> ListClassesWithDateRange
        {
            get => _listclasswithdaterange;
            set
            {
                _listclasswithdaterange = value;
                OnPropertyChanged(nameof(ListClassesWithDateRange)); // Thông báo đúng tên thuộc tính
            }
        }

        public List<Tuple<DateTime, DateTime>> DateRanges
        {
            get => _dateRanges;
            private set
            {
                _dateRanges = value;
                OnPropertyChanged(nameof(DateRanges));
            }
        }
        // ObservableCollection for binding list of classes
        private ObservableCollection<Class> _listclasses = new ObservableCollection<Class>(); // Khởi tạo ở đây
        public ObservableCollection<Class> Listclasses
        {
            get => _listclasses;
            set
            {
                _listclasses = value;
                OnPropertyChanged(nameof(Listclasses)); // Thông báo đúng tên thuộc tính
            }
        }
        public class ClassWithDateRange : Class
        {
            public Tuple<DateTime, DateTime> DateRange { get; set; }
            public int AssignmentCount { get; set; }
            public ClassWithDateRange(string _classname, Tuple<DateTime, DateTime> _dateRange, int _assignmentcount)
            {
                classname = _classname;
                DateRange = _dateRange;
                AssignmentCount = _assignmentcount;
            }
        }

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

        // Constructor with ClassService dependency
        public ClassroomListViewClassesViewModel(ClassesService classService)
        {
            _classService = classService;
            ClassViewModel = new ClassViewModel(_classService);
            InitializeData();
        }

        // Method to create and return ClassesService instance
        private static ClassesService CreateClassService()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=OOP-new,public;");
            var context = new AppDbContext(optionsBuilder.Options);
            return new ClassesService(context);
        }

        // Default constructor that uses CreateClassService
        public ClassroomListViewClassesViewModel() : this(CreateClassService())
        {
        }

        // Method to initialize and load data
        private async void InitializeData()
        {
            try
            {
                // Load classes by student ID
                await ClassViewModel.LoadClassesByStudentIdAsync(StudentContext.Instance.StudentId);

                // Gán cả danh sách các lớp vào ObservableCollection
                Listclasses = new ObservableCollection<Class>(ClassViewModel.Classes);
                ListClassesWithDateRange = new ObservableCollection<ClassWithDateRange> { };
                foreach (var cls in Listclasses)
                {
                    ListClassesWithDateRange.Add(new ClassWithDateRange(cls.classname, new Tuple<DateTime, DateTime>(cls.datebegin, cls.dateend), cls.Assignments.Count));
                    // Hoặc bạn có thể thêm từng lớp một (cách hiện tại):
                    // foreach (var cls in ClassViewModel.Classes)
                    // {
                    //     Listclasses.Add(cls);
                    // }
                }

            }
            catch (Exception ex)
            {
                // Handle errors
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
