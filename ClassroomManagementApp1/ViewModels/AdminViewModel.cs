using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Commands;
using ClassroomManagementApp1.Component;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using ClassroomManagementApp1.Views;  // Giả sử bạn có các View này
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using static ClassroomManagementApp1.ViewModels.ComponentViewModel.AdminStudentInfoViewModel;

namespace ClassroomManagementApp1.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        public ICommand AddStudentCommand { get; }
        public ICommand DeleteStudentCommand { get; }
        public ICommand SaveCommand { get; }
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
        // Thuộc tính SelectedView để lưu trữ View hiện tại
        private object _selectedView;
        public object SelectedView
        {
            get => _selectedView;
            set
            {
                _selectedView = value;
                OnPropertyChanged(nameof(SelectedView)); // Notify UI về thay đổi
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
        // ICommand cho các nút thay đổi view
        public ICommand ChangeViewToTeachersCommand { get; }
        public ICommand ChangeViewToStudentsCommand { get; }
        public ICommand ChangeViewToClassesCommand { get; }
        public AdminViewModel() : this(CreateDbContext())
        {
        }
        // Constructor
        public AdminViewModel(StudentService studentService)
        {
            _studentService = studentService;
            StudentViewModel = new StudentViewModel(_studentService);
            AddStudentCommand = new RelayCommand(_ => AddTemporaryStudent());
            DeleteStudentCommand = new RelayCommand(DeleteSelectedStudent);
            SaveCommand = new RelayCommand(async _ => await SaveChangesToDatabase());
            InitializeData();
        }

        private async void InitializeData()
        {
            Students = new ObservableCollection<Student>();
            await StudentViewModel.LoadAllStudentsAsync();
            var studentslist = StudentViewModel.Students.ToList();
            foreach (var student in studentslist) {
                Students.Add(student);
            }
        }
        // Hàm thay đổi view khi button "Teachers" được nhấn
        private void AddTemporaryStudent()
        {
            var hasEmptyRow = Students.Any(s => string.IsNullOrEmpty(s.studentid));

            if (!hasEmptyRow) // Nếu chưa có dòng trống, thêm dòng mới
            {
                Students.Add(new Student
                {
                    studentid = string.Empty,    // ID trống
                    studentname = string.Empty,  // Tên trống
                    studentemail = string.Empty, // Email trống
                    studentgrade = string.Empty,        // Giá trị mặc định
                    studentbirth = string.Empty
                });
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin dòng trống");
            }
        }
        private void DeleteSelectedStudent(object parameter)
        {
            if (parameter is Student student)
            {
                // Confirm deletion
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa sinh viên {student.studentname} không?",
                                             "Xác nhận",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Students.Remove(student);
                }
            }
            else
            {
                MessageBox.Show("Không có sinh viên nào được chọn.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private async Task SaveChangesToDatabase()
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                foreach (var student in Students)
                {
                    if (string.IsNullOrWhiteSpace(student.studentid) ||
                        string.IsNullOrWhiteSpace(student.studentname) ||
                        string.IsNullOrWhiteSpace(student.studentemail) ||
                        string.IsNullOrWhiteSpace(student.studentgrade) ||
                       string.IsNullOrWhiteSpace(student.studentbirth))// Kiểm tra điểm phải > 0
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin cho tất cả các học sinh trước khi lưu.",
                                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                // Nếu dữ liệu hợp lệ, gọi phương thức lưu vào cơ sở dữ liệu
                await _studentService.UpdateStudentsAsync(Students);

                MessageBox.Show("Thay đổi đã được lưu vào cơ sở dữ liệu.",
                                "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}",
                                "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}