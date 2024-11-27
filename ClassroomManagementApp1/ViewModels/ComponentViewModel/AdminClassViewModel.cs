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
    public class AdminClassViewModel : ViewModelBase
    {
        public ICommand AddClassCommand { get; }
        public ICommand DeleteClassCommand { get; }
        public ICommand SaveCommand { get; }
        private readonly ClassesService _classService;
        public ClassViewModel ClassViewModel { get; private set; }
        private ObservableCollection<Class> _classes;
        public ObservableCollection<Class> Classes
        {
            get { return _classes; }
            set
            {
                _classes = value;
                OnPropertyChanged(nameof(Classes));
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
        private static ClassesService CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=OOP-new,public;");
            var context = new AppDbContext(optionsBuilder.Options);
            var classService = new ClassesService(context);
            return (classService);
        }
        public AdminClassViewModel() : this(CreateDbContext())
        {
        }
        // Constructor
        public AdminClassViewModel(ClassesService classesService)
        {
            _classService = classesService;
            ClassViewModel = new ClassViewModel(_classService);
            AddClassCommand = new RelayCommand(_ => AddTemporaryClass());
            DeleteClassCommand = new RelayCommand(DeleteSelectedClass);
            SaveCommand = new RelayCommand(async _ => await SaveChangesToDatabase());
            InitializeData();
        }

        private async void InitializeData()
        {
            Classes = new ObservableCollection<Class>();
            await ClassViewModel.LoadAllClassAsync();
            var classeslist = ClassViewModel.Classes.ToList();
            foreach (var classe in classeslist)
            {
                Classes.Add(classe);
            }
        }
        // Hàm thay đổi view khi button "Teachers" được nhấn
        private void AddTemporaryClass()
        {
            var hasEmptyRow = Classes.Any(s => string.IsNullOrEmpty(s.classid));

            if (!hasEmptyRow) // Nếu chưa có dòng trống, thêm dòng mới
            {
                Classes.Add(new Class
                {
                    classid = string.Empty,    
                    teacherid = string.Empty,  
                    subjectid = string.Empty,
                    classname = string.Empty,
                    datebegin = new DateTime(1999, 1, 1), // Sử dụng DateTime hợp lệ
                    dateend = new DateTime(1999, 1, 1),   // Sử dụng DateTime hợp lệ

                });
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin dòng trống");
            }
        }
        private void DeleteSelectedClass(object parameter)
        {
            if (parameter is Class classe)
            {
                // Confirm deletion
                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa sinh viên {classe.classname} không?",
                                             "Xác nhận",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Classes.Remove(classe);
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
                foreach (var classe in Classes)
                {
                    if (string.IsNullOrWhiteSpace(classe.classid) ||
                        string.IsNullOrWhiteSpace(classe.teacherid) ||
                        string.IsNullOrWhiteSpace(classe.subjectid) ||
                        classe.datebegin == default || // Kiểm tra ngày bắt đầu
                        classe.dateend == default ||  // Kiểm tra ngày kết thúc
                        classe.dateend < classe.datebegin || // Ngày kết thúc không được trước ngày bắt đầu
                        string.IsNullOrWhiteSpace(classe.classname))
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin cho tất cả các học sinh trước khi lưu.",
                                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                // Nếu dữ liệu hợp lệ, gọi phương thức lưu vào cơ sở dữ liệu
                //await _classService.UpdateStudentsAsync(Classes);

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