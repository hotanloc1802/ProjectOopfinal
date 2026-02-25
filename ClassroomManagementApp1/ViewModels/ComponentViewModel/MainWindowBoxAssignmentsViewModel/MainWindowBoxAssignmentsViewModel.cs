using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using ClassroomManagementApp1.Factory;
using ClassroomManagementApp1.DesignPattern;
using System.Linq;
using ClassroomManagementApp1.Data;

namespace ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxAssignmentsViewModel
{
    public class MainWindowBoxAssignmentsViewModel : ViewModelBase
    {
        private MainWindowBoxAssignmentsItem _item;

        public ClassViewModel ClassViewModel { get; private set; }
        public AssignmentViewModel AssignmentViewModel { get; private set; }
        private readonly ClassesService _classService;
        private readonly AssignmentService _assignmentService;

        private MainWindowBoxAssignmentsItem[] _items = new MainWindowBoxAssignmentsItem[3];

        public MainWindowBoxAssignmentsItem this[int index]
        {
            get => _items[index];
            set
            {
                if (_items[index] != value)
                {
                    _items[index] = value;
                    OnPropertyChanged($"Item{index + 1}");
                }
            }
        }

        public MainWindowBoxAssignmentsItem Item1
        {
            get => this[0];
            set => this[0] = value;
        }

        public MainWindowBoxAssignmentsItem Item2
        {
            get => this[1];
            set => this[1] = value;
        }

        public MainWindowBoxAssignmentsItem Item3
        {
            get => this[2];
            set => this[2] = value;
        }

        public MainWindowBoxAssignmentsViewModel(ClassesService classService, AssignmentService assignmentService)
        {
            _classService = classService;
            _assignmentService = assignmentService;
            ClassViewModel = new ClassViewModel(_classService);
            AssignmentViewModel = new AssignmentViewModel(_assignmentService);
            InitializeData();
        }

        public MainWindowBoxAssignmentsViewModel()
            : this(ServiceFactory.CreateClassesService(), ServiceFactory.CreateAssignmentService())
        {
        }

        private async void InitializeData()
        {
            try
            {
                await ClassViewModel.LoadTop3NearestClassesByStudentIdAsync(StudentContextSingleton.Instance.StudentId);
                var classList = ClassViewModel.Classes.ToList();

                for (int i = 0; i < Math.Min(3, classList.Count); i++)
                {
                    await AssignmentViewModel.LoadNearestAssignmentByClassIDAsync(classList[i].classid);
                    var nearestAssignment = AssignmentViewModel.NearestAssignment;

                    if (nearestAssignment != null)
                    {
                        var formattedDate = FormatDate(nearestAssignment.duedate);
                        this[i] = new MainWindowBoxAssignmentsItem(nearestAssignment.description, formattedDate);
                    }
                    else
                    {
                        this[i] = new MainWindowBoxAssignmentsItem("No Assignment Available", string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors if any
                //MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string FormatDate(DateTime dueDate)
        {
            return dueDate.ToString("dddd, MMMM d") + GetDaySuffix(dueDate.Day) + dueDate.ToString(", yyyy");
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
