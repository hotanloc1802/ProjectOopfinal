using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Factory;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.DesignPattern;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;

namespace ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxClassesViewModel
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

        private Class _classInfo;
        public Class ClassInfo
        {
            get => _classInfo;
            set
            {
                _classInfo = value;
                OnPropertyChanged(nameof(ClassInfo));
            }
        }

        public SubmissionViewModel SubmissionViewModel { get; private set; }
        public ClassViewModel ClassViewModel { get; private set; }
        public AssignmentViewModel AssignmentViewModel { get; private set; }
        private readonly ClassesService _classService;
        private readonly AssignmentService _assignmentService;
        private readonly SubmissionService _submissionService;

        // ObservableCollection for data binding
        public ObservableCollection<MainWindowBoxClassesItem> _listitem = new ObservableCollection<MainWindowBoxClassesItem>();
        public ObservableCollection<MainWindowBoxClassesItem> ListItem
        {
            get => _listitem;
            set
            {
                _listitem = value;
                OnPropertyChanged(nameof(ListItem));
            }
        }

        public MainWindowBoxClassesViewModel( AssignmentService assignmentService,ClassesService classService, SubmissionService submissionService)
        {
            _classService = classService;
            _assignmentService = assignmentService;
            _submissionService = submissionService;
            ClassViewModel = new ClassViewModel(_classService);
            AssignmentViewModel = new AssignmentViewModel(_assignmentService);
            SubmissionViewModel = new SubmissionViewModel(_submissionService);
            InitializeData();
        }

        public MainWindowBoxClassesViewModel() : this(ServiceFactory.CreateAssignmentService(), ServiceFactory.CreateClassesService(), ServiceFactory.CreateSubmissionService())
        {
        }
        private async void InitializeData()
        {
            try
            {
                // Load the top 3 nearest classes for the student and display assignments
                await ClassViewModel.LoadTop3NearestClassesByStudentIdAsync(StudentContextSingleton.Instance.StudentId);
                var ClassList = ClassViewModel.Classes.ToList(); // Contains top 3 classes

                foreach (var cls in ClassList)
                {
                    int countNotSubmitted = 0;
                    await ClassViewModel.LoadAssignmentsByClassIdAsync(cls.classid); // Get all assignments for class ID
                    var assignmentsList = ClassViewModel.Assignments.ToList();
                    SetDateRange(cls.datebegin, cls.dateend);
                    await SubmissionViewModel.LoadSubmissionsByStudentId(StudentContextSingleton.Instance.StudentId);
                    var submissionList = SubmissionViewModel.Submissions;

                    foreach (var asm in assignmentsList)
                    {
                        bool isSubmitted = submissionList.Any(sms => sms.assignmentid == asm.assignmentid);
                        if (!isSubmitted)
                        {
                            countNotSubmitted++;
                        }
                    }

                    try
                    {
                        ListItem.Add(new MainWindowBoxClassesItem(
                            cls.Subject.subjectname,
                            cls.Teacher.teachername,
                            countNotSubmitted,
                            cls.datebegin.ToString("dd/MM/yyyy"),
                            cls.dateend.ToString("dd/MM/yyyy"),
                            cls.classid
                        ));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error while loading submissions: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors if any
                //MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SetDateRange(DateTime startDate, DateTime endDate)
        {
            DateRanges.Add(new Tuple<DateTime, DateTime>(startDate, endDate));
            OnPropertyChanged(nameof(DateRanges)); // Notify that DateRanges has changed
        }
    }
}
