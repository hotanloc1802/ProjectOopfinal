using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Factory;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.DesignPattern;
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

        public ClassInfoViewModel(ClassesService classService, AssignmentService assignmentService, SubmissionService submissionService, string classID)
        {
            _classService = classService;
            _assignmentService = assignmentService;
            _submissionService = submissionService;
            ClassViewModel = new ClassViewModel(_classService);
            SubmissionViewModel = new SubmissionViewModel(_submissionService);
            AssignmentViewModel = new AssignmentViewModel(_assignmentService);
            InitializeData(classID);
        }
      
        public ClassInfoViewModel(string classID)
            : this(ServiceFactory.CreateClassesService(), ServiceFactory.CreateAssignmentService(), ServiceFactory.CreateSubmissionService() ,classID)
        {
        }

        private async void InitializeData(string classID)
        {
            await ClassViewModel.LoadClassByIdAsync(classID);
            try
            {
                await SubmissionViewModel.LoadSubmissionsByStudentId(StudentContextSingleton.Instance.StudentId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading submissions: {ex.Message}");
            }

            ClassInfo = ClassViewModel.SelectedClass;
            var submissionList = SubmissionViewModel.Submissions;
            var assignmentsList = ClassViewModel.Assignments.ToList();
            // Format the assignments and add them to the formatted list
            foreach (var asm in assignmentsList)
            {
                var date = asm.duedate.ToString("dd/MM/yyyy");
                AssignmentsFormattedList.Add(new AssignmentFormated(date, asm.description));
            }

            // Find assignments that are not submitted and add them to AssignmentNotSubmitted
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
