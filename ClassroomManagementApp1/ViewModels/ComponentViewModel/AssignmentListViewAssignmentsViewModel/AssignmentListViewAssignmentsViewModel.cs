using ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxAssignmentsViewModel;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
using ClassroomManagementApp1.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ClassroomManagementApp1.ViewModels.ComponentViewModel.AssignmentListViewAssignmentsViewModel
{
    public class AssignmentListViewAssignmentsViewModel : ViewModelBase
    {
        private MainWindowBoxAssignmentsItem _item;

        public AssignmentViewModel AssignmentViewModel { get; private set; }
        private ObservableCollection<item> _firstHalfAssignments = new ObservableCollection<item>();
        private ObservableCollection<item> _secondHalfAssignments = new ObservableCollection<item>();

        public class item
        {
            public string _assignmentname { get; set; }
            public string _description { get; set; }
            public string _duedate { get; set; }
            public item() { }
            public item(string assignmentname, string description, string duedate)
            {
                _assignmentname = assignmentname;
                _description = description;
                _duedate = duedate;
            }
        }

        public ObservableCollection<item> FirstHalfAssignments
        {
            get => _firstHalfAssignments;
            set
            {
                _firstHalfAssignments = value;
                OnPropertyChanged(nameof(FirstHalfAssignments));
            }
        }

        public ObservableCollection<item> SecondHalfAssignments
        {
            get => _secondHalfAssignments;
            set
            {
                _secondHalfAssignments = value;
                OnPropertyChanged(nameof(SecondHalfAssignments));
            }
        }

        private ObservableCollection<Assignment> _listassignments = new ObservableCollection<Assignment>();
        public ObservableCollection<Assignment> Listassignments
        {
            get => _listassignments;
            set
            {
                _listassignments = value;
                OnPropertyChanged(nameof(Listassignments));
            }
        }
        private readonly IAssignmentService _assignmentService;

        public MainWindowBoxAssignmentsItem Item
        {
            get => _item;
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
            }
        }

        public AssignmentListViewAssignmentsViewModel(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
            AssignmentViewModel = new AssignmentViewModel(_assignmentService);
            InitializeData();
        }

        public AssignmentListViewAssignmentsViewModel() : this(App.Services.GetRequiredService<IAssignmentService>())
        {
        }

        private async void InitializeData()
        {
            try
            {
                var studentId = App.Services.GetRequiredService<ICurrentStudentContext>().StudentId;
                if (string.IsNullOrWhiteSpace(studentId)) return;

                await AssignmentViewModel.LoadAssignmentsByStudentId(studentId);

                if (AssignmentViewModel.Assignments != null && AssignmentViewModel.Assignments.Any())
                {
                    Listassignments = new ObservableCollection<Assignment>(AssignmentViewModel.Assignments);

                    int halfCount = Listassignments.Count / 2;

                    // Tách danh sách assignment thành hai phần
                    var store1 = Listassignments.Take(halfCount);
                    var store2 = Listassignments.Skip(halfCount);

                    FirstHalfAssignments = FormatAssignments(store1);
                    SecondHalfAssignments = FormatAssignments(store2);
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu bài tập nào được tải về.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Hàm định dạng assignment và chuyển sang ObservableCollection<item>
        private ObservableCollection<item> FormatAssignments(IEnumerable<Assignment> assignments)
        {
            var formattedAssignments = new ObservableCollection<item>();

            foreach (var asm in assignments)
            {
                if (asm.Class != null && asm.duedate != null)
                {
                    string formattedDate = asm.duedate.ToString("dddd, MMMM d") + GetDaySuffix(asm.duedate.Day) + asm.duedate.ToString(", yyyy");
                    formattedAssignments.Add(new item(asm.Class.classname, asm.description, formattedDate));
                }
                else
                {
                    MessageBox.Show("Một hoặc nhiều thuộc tính của bài tập không hợp lệ.", "Lỗi dữ liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            return formattedAssignments;
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
