using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ClassroomManagementApp1.Component;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
using ClassroomManagementApp1.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ClassroomManagementApp1.ViewModels
{
    public class ClassesViewModel : ViewModelBase
    {
        public ClassViewModel ClassViewModel { get; private set; }
        public AssignmentViewModel AssignmentViewModel { get; private set; }
        public SubmissionViewModel SubmissionViewModel { get; private set; }
        private readonly IClassesService _classService;
        private readonly IAssignmentService _assignmentService;
        private readonly ISubmissionService _submissionService;
        public ObservableCollection<Assignment> AssignmentNotSubmitted { get; set; } = new ObservableCollection<Assignment>();
        public ObservableCollection<SubmissionAsignment> SubmissonAssignments { get; private set; } = new ObservableCollection<SubmissionAsignment>();
        private string _selectedFilePath;
        public string SelectedFilePath
        {
            get => _selectedFilePath;
            set
            {
                _selectedFilePath = value;
                OnPropertyChanged(nameof(SelectedFilePath));
            }
        }

        private string _subject;
        public string Subject
        {
            get => _subject;
            set
            {
                _subject = value;
                OnPropertyChanged(nameof(Subject));
            }
        }
        private string _myclassid;
        public string MyClassId
        {
            get => _myclassid;
            set
            {
                _myclassid = value;
                OnPropertyChanged(nameof(MyClassId));
            }
        }
        public class SubmissionAsignment
        {
            public string duedate { get; set; }
            public string classname { get; set; }
            public string description { get; set; }
            public string linksubmission { get; set; }
            public Assignment Assignment { get; set; }
            public SubmissionAsignment(string _classname, string _description, string _duedate)
            {
                classname = _classname;
                description = _description;
                duedate = _duedate;

            }

        }
        public ClassesViewModel(IAssignmentService assignmentService, ISubmissionService submissionService, IClassesService classesService, string classID)
        {
            _classService = classesService;
            _assignmentService = assignmentService;
            _submissionService = submissionService;
            SubmissionViewModel = new SubmissionViewModel(_submissionService);
            AssignmentViewModel = new AssignmentViewModel(_assignmentService);
            ClassViewModel = new ClassViewModel(_classService);
            MyClassId = classID;
            InitializeData(classID);
        }
        public ClassesViewModel(string classID)
            : this(
                App.Services.GetRequiredService<IAssignmentService>(),
                App.Services.GetRequiredService<ISubmissionService>(),
                App.Services.GetRequiredService<IClassesService>(),
                classID)
        {
        }
        private async void InitializeData(string classID)
        {
            await ClassViewModel.LoadClassByIdAsync(classID);
            var className = ClassViewModel.SelectedClass.Subject.subjectname;
            var assignmentList = ClassViewModel.Assignments;
            var studentId = App.Services.GetRequiredService<ICurrentStudentContext>().StudentId;
            if (!string.IsNullOrWhiteSpace(studentId))
            {
                await SubmissionViewModel.LoadSubmissionsByStudentId(studentId);
            }
            var submissionList = SubmissionViewModel.Submissions;
            Subject = ClassViewModel.SelectedClass.Subject.subjectname;
            foreach (var asm in assignmentList)
            {
                bool isSubmitted = submissionList.Any(sms => sms.assignmentid == asm.assignmentid);
                if (!isSubmitted)
                {
                    AssignmentNotSubmitted.Add(asm);
                }
            }
            foreach (var asm in AssignmentNotSubmitted)
            {
                //await SubmissionViewModel.LoadSubmissionsByStudentIdAndAssignmentId(StudentContext.Instance.StudentId, asm.assignmentid);
                //var submission = SubmissionViewModel.SelectedSubmission;
                DateTime dueDate = asm.duedate;
                string formattedDate = dueDate.ToString("dddd, MMMM d") + GetDaySuffix(dueDate.Day) + dueDate.ToString(", yyyy");
                //if (submission != null)
                //{
                SubmissonAssignments.Add(new SubmissionAsignment(className, asm.description, formattedDate));
                //}
            }

        }
        private string GetDaySuffix(int day)
        {
            if (day >= 11 && day <= 13) return "th";
            return (day % 10) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th",
            };
        }


    }
}