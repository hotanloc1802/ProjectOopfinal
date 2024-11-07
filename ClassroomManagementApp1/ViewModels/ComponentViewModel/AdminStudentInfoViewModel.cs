using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomManagementApp1.ViewModels.ComponentViewModel
{
    public class AdminStudentInfoViewModel : ViewModelBase
    {
        public StudentViewModel StudentViewModel { get; private set; }
        private ObservableCollection<StudentItem> _students;
        public ObservableCollection<StudentItem> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged(nameof(Students));
            }
        }

        public class StudentItem
        {
            public string StudentId { get; set; }
            public string StudentName { get; set; }
            public string Email { get; set; }
            public int StudentGrade { get; set; }

            public StudentItem() { }

            public StudentItem(string studentId, string studentName, string email, int studentGrade)
            {
                StudentId = studentId;
                StudentName = studentName;
                Email = email;
                StudentGrade = studentGrade;
            }
        }
        private readonly StudentService _studentService;
        public AdminStudentInfoViewModel(StudentService studentService)
        {
            _studentService = studentService;
            StudentViewModel = new StudentViewModel(_studentService);
            InitializeData();
        }

        public AdminStudentInfoViewModel()
        {
        }

        // Method to create and return ClassesService instance
        private static AssignmentService CreateAssignmenService()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=OOP-new,public;");
            var context = new AppDbContext(optionsBuilder.Options);
            return new AssignmentService(context);
        }
        // Phương thức để khởi tạo dữ liệu từ database
        private async void InitializeData()
        {
            // Lấy danh sách sinh viên từ StudentService
            var studentList = await _studentService.GetAllStudentsAsync();

            // Khởi tạo ObservableCollection với dữ liệu lấy được
            Students = new ObservableCollection<StudentItem>(studentList.Select(student => new StudentItem
            {
                StudentId = student.studentid,
                StudentName = student.studentname,
                Email = student.studentemail,
                StudentGrade = student.studentgrade
            }));
        }

    }
}
