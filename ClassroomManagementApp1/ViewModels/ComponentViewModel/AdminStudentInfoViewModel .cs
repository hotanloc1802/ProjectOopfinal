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
using System.Windows;
using ClassroomManagementApp1.Commands;
using System.Windows.Input;
namespace ClassroomManagementApp1.ViewModels.ComponentViewModel
{
    public class AdminStudentInfoViewModel : ViewModelBase
    {
        private readonly StudentService _studentService;
        public StudentViewModel StudentViewModel { get; private set; }
        private ObservableCollection<Student> _students;
        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged(nameof(Students));
            }
        }
   
        // 6. Khởi tạo MainWindowViewModel
        private static StudentService CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=OOP-new,public;");
            var context = new AppDbContext(optionsBuilder.Options);
            var studentService = new StudentService(context);
            return (studentService);
        }
        public AdminStudentInfoViewModel() : this(CreateDbContext())
        {
        }
        // Constructor
        public AdminStudentInfoViewModel(StudentService studentService)
        {
            _studentService = studentService;
            StudentViewModel = new StudentViewModel(_studentService);
            InitializeData();
        }

        private async void InitializeData()
        {
            Students = new ObservableCollection<Student>();
            await StudentViewModel.LoadAllStudentsAsync();
            var studentslist = StudentViewModel.Students.ToList();
            foreach (var student in studentslist)
            {
                Students.Add(student);
            }
        }
       
    }
}