using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using Microsoft.EntityFrameworkCore;

namespace ClassroomManagementApp1.ViewModels
{
    public class ClassInfoViewModel : ViewModelBase
    {
        public ClassViewModel ClassViewModel { get; private set; }
        public AssignmentViewModel AssignmentViewModel { get; private set; }
        public SubmissionViewModel SubmissionViewModel { get; private set; }
        private readonly ClassesService _classService;
        private readonly AssignmentService _assignmentService;
        private readonly SubmissionService _submissionService;

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

        public class AssignmentFormated : Assignment
        {
            public string Date { get; set; }
            public AssignmentFormated(string date, string description)
            {
                this.description = description;
                Date = date;
            }
        }


        public ObservableCollection<AssignmentFormated> AssignmentsFormattedList { get; set; } = new ObservableCollection<AssignmentFormated>();
        public ObservableCollection<Assignment> AssignmentNotSubmitted { get; set; } = new ObservableCollection<Assignment>();

        public ClassInfoViewModel(ClassesService classService, AssignmentService assignmentService, SubmissionService submissionService,string classID)
        {
            _classService = classService;
            _assignmentService = assignmentService;
           _submissionService = submissionService;
            ClassViewModel = new ClassViewModel(_classService);
           SubmissionViewModel = new SubmissionViewModel(_submissionService);
            AssignmentViewModel = new AssignmentViewModel(_assignmentService);
            InitializeData(classID);
        }

        // Phương thức tạo context kết nối với cơ sở dữ liệu
        private static (ClassesService, AssignmentService, SubmissionService) CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=OOP-new,public;");
            var context = new AppDbContext(optionsBuilder.Options);

            var classesService = new ClassesService(context);
            var assignmentService = new AssignmentService(context);
            var submissionService = new SubmissionService(context);

            return (classesService, assignmentService,submissionService);
        }

        public ClassInfoViewModel(string classID)
            : this(CreateDbContext().Item1, CreateDbContext().Item2, CreateDbContext().Item3 ,classID)
        {
        }

        private async void InitializeData(string classID)
        {
           await ClassViewModel.LoadClassByIdAsync(classID);
           await SubmissionViewModel.LoadSubmissionsByStudentId(StudentContext.Instance.StudentId);

            ClassInfo = ClassViewModel.SelectedClass;
            var submissionList = SubmissionViewModel.Submissions;
            var assignmentsList = ClassInfo.Assignments;
            // Định dạng các bài tập và thêm vào danh sách định dạng
            foreach (var asm in assignmentsList)
            {
                var date = asm.duedate.ToString("dd/MM/yyyy");
                AssignmentsFormattedList.Add(new AssignmentFormated(date, asm.description));
            }

            // Tìm các bài tập chưa được nộp và thêm vào AssignmentNotSubmitted
            foreach (var asm in assignmentsList)
            {
                bool isSubmitted = submissionList.Any(sms => sms.assignmentid == asm.assignmentid);
                if (!isSubmitted)
                {
                    AssignmentNotSubmitted.Add(asm);
                }
            }
        }
    }
}
