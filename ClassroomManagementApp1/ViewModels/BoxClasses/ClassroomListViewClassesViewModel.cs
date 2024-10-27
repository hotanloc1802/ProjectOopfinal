using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ClassroomManagementApp1.ViewModels.BoxClasses
{
    public class ClassroomListViewClassesViewModel : ViewModelBase
    {
        private MainWindowBoxClassesItem _item; // Private field for backing store
        public ClassViewModel ClassViewModel { get; private set; }
        private readonly ClassesService _classService;

        // ObservableCollection for binding list of classes
        private ObservableCollection<Class> _listclasses = new ObservableCollection<Class>(); // Khởi tạo ở đây
        public ObservableCollection<Class> Listclasses
        {
            get => _listclasses;
            set
            {
                _listclasses = value;
                OnPropertyChanged(nameof(Listclasses)); // Thông báo đúng tên thuộc tính
            }
        }

        // Public property for data binding
        public MainWindowBoxClassesItem Item
        {
            get => _item;
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item)); // Notify that Item has changed
            }
        }

        // Constructor with ClassService dependency
        public ClassroomListViewClassesViewModel(ClassesService classService)
        {
            _classService = classService;
            ClassViewModel = new ClassViewModel(_classService);
            InitializeData();
        }

        // Method to create and return ClassesService instance
        private static ClassesService CreateClassService()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=OOP-new,public;");
            var context = new AppDbContext(optionsBuilder.Options);
            return new ClassesService(context);
        }

        // Default constructor that uses CreateClassService
        public ClassroomListViewClassesViewModel() : this(CreateClassService())
        {
        }

        // Method to initialize and load data
        private async void InitializeData()
        {
            try
            {
                // Load classes by student ID
                await ClassViewModel.LoadClassesByStudentIdAsync("S001");

                // Gán cả danh sách các lớp vào ObservableCollection
                Listclasses = new ObservableCollection<Class>(ClassViewModel.Classes);

                // Hoặc bạn có thể thêm từng lớp một (cách hiện tại):
                // foreach (var cls in ClassViewModel.Classes)
                // {
                //     Listclasses.Add(cls);
                // }
            }
            catch (Exception ex)
            {
                // Handle errors
                MessageBox.Show($"Có lỗi xảy ra khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
