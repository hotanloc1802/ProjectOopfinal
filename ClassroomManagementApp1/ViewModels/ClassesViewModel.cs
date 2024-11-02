using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Component;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using Microsoft.EntityFrameworkCore;

namespace ClassroomManagementApp1.ViewModels
{
    public class ClassesViewModel : ViewModelBase
    {
        public ClassViewModel ClassViewModel { get; private set; }
        public AssignmentViewModel AssignmentViewModel { get; private set; }
        public SubmissionViewModel SubmissionViewModel { get; private set; }
        private readonly ClassesService _classService;
        private readonly AssignmentService _assignmentService;
        private readonly SubmissionService _submissionService;
        public ObservableCollection<SubmissionAsignment> SubmissonAssignments { get; private set; } = new ObservableCollection<SubmissionAsignment>();


        public class SubmissionAsignment 
        {
            string duedate { get; set; }
            string classname { get; set; }  
            string description { get; set; }
            string linksubmission { get; set; }
            public Assignment Assignment { get; set; }
            public SubmissionAsignment(string _classname, string _description, string _linksubmission, string _duedate)
            {
                classname = _classname;
                description = _description;
                linksubmission = _linksubmission;
                duedate = _duedate;

            }

        }
        public ClassesViewModel(AssignmentService assignmentService, SubmissionService submissionService, ClassesService classesService, string classID)
        {
            _classService = classesService;
            _assignmentService = assignmentService;
            _submissionService = submissionService;
            SubmissionViewModel = new SubmissionViewModel(_submissionService);
            AssignmentViewModel = new AssignmentViewModel(_assignmentService);
            ClassViewModel = new ClassViewModel(_classService);
            
            InitializeData(classID);
        }
        private static (AssignmentService, SubmissionService, ClassesService) CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=OOP-new,public;");
            var context = new AppDbContext(optionsBuilder.Options);
            var assignmentService = new AssignmentService(context);
            var submissionService = new SubmissionService(context);
            var classesService = new ClassesService(context);
            return (assignmentService, submissionService,classesService);
        }

        public ClassesViewModel(string classID)
            : this(CreateDbContext().Item1, CreateDbContext().Item2, CreateDbContext().Item3 ,classID)
        {
        }
        private async void InitializeData(string classID)
        {
            await ClassViewModel.LoadClassByIdAsync(classID);
            var className = ClassViewModel.SelectedClass.Subject.subjectname;
            var assignmentList = ClassViewModel.Assignments;

            foreach (var asm in assignmentList)
            {
                await SubmissionViewModel.LoadSubmissionsByStudentIdAndAssignmentId("S001", asm.assignmentid);
                var submission = SubmissionViewModel.SelectedSubmission;

                if (submission != null)
                {
                    SubmissonAssignments.Add(new SubmissionAsignment (className, asm.description, submission.linksubmisson, asm.duedate.ToString()));
                }
            }
        }
    }
}
