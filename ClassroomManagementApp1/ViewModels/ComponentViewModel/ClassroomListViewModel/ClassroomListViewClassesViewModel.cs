using ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxClassesViewModel;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
using ClassroomManagementApp1.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ClassroomManagementApp1.ViewModels.ComponentViewModel.ClassroomListViewModel
{
    public class ClassroomListViewClassesViewModel : ViewModelBase
    {
        private MainWindowBoxClassesItem _item; // Private field for backing store
        public ClassViewModel ClassViewModel { get; private set; }
        public SubmissionViewModel SubmissionViewModel { get; private set; }
        private readonly IClassesService _classService;
        private readonly ISubmissionService _submissionService;
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
            public ClassWithDateRange(string _classid, string _classname, Tuple<DateTime, DateTime> _dateRange, int _assignmentcount)
            {
                classid = _classid;
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
        public ClassroomListViewClassesViewModel(IClassesService classService, ISubmissionService submissionService)
        {
            _classService = classService;
            ClassViewModel = new ClassViewModel(_classService);
            _submissionService = submissionService;
            SubmissionViewModel = new SubmissionViewModel(_submissionService);
            InitializeData();
        }

        // Default constructor that uses CreateClassService
        public ClassroomListViewClassesViewModel() : this(
            App.Services.GetRequiredService<IClassesService>(),
            App.Services.GetRequiredService<ISubmissionService>())
        {
        }

        // Method to initialize and load data
        private async void InitializeData()
        {
            try
            {
                var studentId = App.Services.GetRequiredService<ICurrentStudentContext>().StudentId;
                if (string.IsNullOrWhiteSpace(studentId)) return;

                await ClassViewModel.LoadClassesByStudentIdAsync(studentId);
                // Gán cả danh sách các lớp vào ObservableCollection
                Listclasses = new ObservableCollection<Class>(ClassViewModel.Classes);
                ListClassesWithDateRange = new ObservableCollection<ClassWithDateRange> { };
                foreach (var cls in Listclasses)
                {
                    await ClassViewModel.LoadAssignmentsByClassIdAsync(cls.classid);
                    var assignmentsList = ClassViewModel.Assignments.ToList();
                    await SubmissionViewModel.LoadSubmissionsByStudentId(studentId);
                    var submissionList = SubmissionViewModel.Submissions;
                    int countNotSubmitted = 0;
                    foreach (var asm in assignmentsList)
                    {
                        bool isSubmitted = submissionList.Any(sms => sms.assignmentid == asm.assignmentid);
                        if (!isSubmitted)
                        {
                            countNotSubmitted++;
                        }
                    }
                    ListClassesWithDateRange.Add(new ClassWithDateRange(cls.classid, cls.classname, new Tuple<DateTime, DateTime>(cls.datebegin, cls.dateend), countNotSubmitted));
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