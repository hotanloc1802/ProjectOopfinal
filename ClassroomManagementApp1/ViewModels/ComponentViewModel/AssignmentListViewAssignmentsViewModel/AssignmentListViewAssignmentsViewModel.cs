using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxAssignmentsViewModel;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClassroomManagementApp1.ViewModels.ComponentViewModel.AssignmentListViewAssignmentsViewModel
{
    public class AssignmentListViewAssignmentsViewModel : ViewModelBase
    {
        private MainWindowBoxAssignmentsItem _item; // Private field for backing store

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
                OnPropertyChanged(nameof(FirstHalfAssignments)); // Thông báo đúng tên thuộc tính
            }
        }

        public ObservableCollection<item> SecondHalfAssignments
        {
            get => _secondHalfAssignments;
            set
            {
                _secondHalfAssignments = value;
                OnPropertyChanged(nameof(SecondHalfAssignments)); // Thông báo đúng tên thuộc tính
            }
        }

        private ObservableCollection<Assignment> _listassignments = new ObservableCollection<Assignment>();
        public ObservableCollection<Assignment> Listassignments
        {
            get => _listassignments;
            set
            {
                _listassignments = value;
                OnPropertyChanged(nameof(Listassignments)); // Thông báo đúng tên thuộc tính
            }
        }
        private readonly AssignmentService _assignmentService;

        // Public property for data binding
        public MainWindowBoxAssignmentsItem Item
        {
            get => _item;
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item)); // Notify that Item has changed
            }
        }
        public AssignmentListViewAssignmentsViewModel(AssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
            AssignmentViewModel = new AssignmentViewModel(_assignmentService);
            InitializeData();
        }

        // Method to create and return ClassesService instance
        private static AssignmentService CreateAssignmenService()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=OOP-new,public;");
            var context = new AppDbContext(optionsBuilder.Options);
            return new AssignmentService(context);
        }

        // Default constructor that uses CreateClassService
        public AssignmentListViewAssignmentsViewModel() : this(CreateAssignmenService())
        {
        }
        private async void InitializeData()
        {
            try
            {
                await AssignmentViewModel.LoadAssignmentsByStudentId(StudentContext.Instance.StudentId);

                // Kiểm tra xem Assignments có null không
                if (AssignmentViewModel.Assignments != null)
                {
                    Listassignments = new ObservableCollection<Assignment>(AssignmentViewModel.Assignments);
                    int halfCount = Listassignments.Count / 2;

                    var store1 = new ObservableCollection<Assignment>(Listassignments.Take(halfCount));
                    var store2 = new ObservableCollection<Assignment>(Listassignments.Skip(halfCount).Take(halfCount));
                    FirstHalfAssignments = new ObservableCollection<item>();
                    SecondHalfAssignments = new ObservableCollection<item>();

                    foreach (var asm1 in store1)
                    {
                        // Kiểm tra Class và duedate không phải null
                        if (asm1.Class != null && asm1.duedate != null)
                        {
                            string formattedDate1 = asm1.duedate.ToString("dddd, MMMM d") + GetDaySuffix(asm1.duedate.Day) + asm1.duedate.ToString(", yyyy");
                            FirstHalfAssignments.Add(new item(asm1.Class.classname, asm1.description, formattedDate1));
                        }
                        else
                        {
                            // Xử lý khi Class hoặc duedate là null
                            MessageBox.Show("Một hoặc nhiều thuộc tính của bài tập không hợp lệ.", "Lỗi dữ liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }

                    foreach (var asm2 in store2)
                    {
                        // Kiểm tra Class và duedate không phải null
                        if (asm2.Class != null && asm2.duedate != null)
                        {
                            string formattedDate2 = asm2.duedate.ToString("dddd, MMMM d") + GetDaySuffix(asm2.duedate.Day) + asm2.duedate.ToString(", yyyy");
                            SecondHalfAssignments.Add(new item(asm2.Class.classname, asm2.description, formattedDate2));
                        }
                        else
                        {
                            // Xử lý khi Class hoặc duedate là null
                            MessageBox.Show("Một hoặc nhiều thuộc tính của bài tập không hợp lệ.", "Lỗi dữ liệu", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Không có dữ liệu bài tập nào được tải về.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show($"Có lỗi xảy ra khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
