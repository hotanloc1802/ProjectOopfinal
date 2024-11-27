using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Commands;
using ClassroomManagementApp1.Component;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using ClassroomManagementApp1.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClassroomManagementApp1.ViewModels
{
    public class AdminTeacherViewModel : ViewModelBase
    {
        public ICommand AddTeacherCommand { get; }
        public ICommand DeleteTeacherCommand { get; }
        public ICommand SaveCommand { get; }
        private readonly TeacherService _teacherService;
        public TeacherViewModel TeacherViewModel { get; private set; }
        private ObservableCollection<Teacher> _teachers;
        public ObservableCollection<Teacher> Teachers
        {
            get { return _teachers; }
            set
            {
                _teachers = value;
                OnPropertyChanged(nameof(Teachers));
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
                OnPropertyChanged(nameof(SelectedView));
            }
        }

        // 6. Khởi tạo MainWindowViewModel
        private static TeacherService CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=OOP-new,public;");
            var context = new AppDbContext(optionsBuilder.Options);
            var teacherService = new TeacherService(context);
            return teacherService;
        }

        public AdminTeacherViewModel() : this(CreateDbContext())
        {
        }

        // Constructor
        public AdminTeacherViewModel(TeacherService teacherService)
        {
            _teacherService = teacherService;
            TeacherViewModel = new TeacherViewModel(_teacherService);
            AddTeacherCommand = new RelayCommand(_ => AddTemporaryTeacher());
            DeleteTeacherCommand = new RelayCommand(DeleteSelectedTeacher);
            SaveCommand = new RelayCommand(async _ => await SaveChangesToDatabase());
            InitializeData();
        }

        private async void InitializeData()
        {
            Teachers = new ObservableCollection<Teacher>();
            await TeacherViewModel.LoadTeachers();
            var teachersList = TeacherViewModel.Teachers.ToList();
            foreach (var teacher in teachersList)
            {
                Teachers.Add(teacher);
            }
        }

        private void AddTemporaryTeacher()
        {
            var hasEmptyRow = Teachers.Any(t => string.IsNullOrEmpty(t.teacherid));

            if (!hasEmptyRow) // Nếu chưa có dòng trống, thêm dòng mới
            {
                Teachers.Add(new Teacher
                {
                    teacherid = string.Empty,
                    teachername = string.Empty,
                    teacheremail = string.Empty,
                });
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin dòng trống");
            }
        }

        private void DeleteSelectedTeacher(object parameter)
        {
            if (parameter is Teacher teacher)
            {
                // Confirm deletion
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa giáo viên {teacher.teachername} không?",
                                             "Xác nhận",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Teachers.Remove(teacher);
                }
            }
            else
            {
                MessageBox.Show("Không có giáo viên nào được chọn.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async Task SaveChangesToDatabase()
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                foreach (var teacher in Teachers)
                {
                    if (string.IsNullOrWhiteSpace(teacher.teacherid) ||
                        string.IsNullOrWhiteSpace(teacher.teachername) ||
                        string.IsNullOrWhiteSpace(teacher.teacheremail))

                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin cho tất cả giáo viên trước khi lưu.",
                                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                // Nếu dữ liệu hợp lệ, gọi phương thức lưu vào cơ sở dữ liệu
                //await _teacherService.UpdateTeachersAsync(Teachers);

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
