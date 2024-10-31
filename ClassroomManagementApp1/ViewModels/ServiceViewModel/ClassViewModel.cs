using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace ClassroomManagementApp1.ViewModels.ServiceViewModels
{
    public class ClassViewModel : INotifyPropertyChanged
    {
        private readonly ClassesService _classService;

        // ObservableCollection to notify UI of changes
        public ObservableCollection<Class> Classes { get; set; } = new ObservableCollection<Class>();
        public ObservableCollection<Assignment> Assignments { get; set; } = new ObservableCollection<Assignment>();
        public ObservableCollection<HashSet<Tuple<string, string>>> StudentSubmisson { get; set; } = new ObservableCollection<HashSet<Tuple<string, string>>>();

        private Class _selectedClass; // Chỉ sử dụng một thuộc tính cho lớp học đã chọn
        public Class SelectedClass
        {
            get => _selectedClass;
            set
            {
                _selectedClass = value;
                OnPropertyChanged(nameof(SelectedClass)); // Thông báo UI về sự thay đổi
            }
        }

        public ClassViewModel(ClassesService classService)
        {
            _classService = classService;
        }

        // Tải tất cả các lớp học
        public async Task LoadAllClassesAsync(string studentid)
        {
            var classList = await _classService.GetAllClassesByStudentId(studentid);
            Classes.Clear();
            foreach (var cls in classList)
            {
                Classes.Add(cls);
            }
        }

        // Tải 3 lớp gần nhất theo StudentID
        public async Task LoadTop3NearestClassesByStudentIdAsync(string studentId)
        {
            var classList = await _classService.GetTop3NearestClassesByStudentId(studentId);
            Classes.Clear();
            foreach (var cls in classList)
            {
                Classes.Add(cls);
            }
        }

        // Lấy thông tin của một lớp học theo ClassID
        public async Task LoadClassByIdAsync(string classId)
        {
            var cls = await _classService.GetClassById(classId);
            if (cls != null) // Kiểm tra xem lớp học có tồn tại không
            {
                SelectedClass = cls; // Gán lớp học vào property để hiển thị
                Assignments.Clear(); // Xóa các bài tập cũ
                foreach (var asm in cls.Assignments)
                {
                    Assignments.Add(asm); // Thêm bài tập vào danh sách
                }
            }
            else
            {
                // Xử lý nếu lớp học không tồn tại (tuỳ theo yêu cầu của bạn)
                SelectedClass = null;
            }
        }

        public async Task LoadClassesByStudentIdAsync(string studentId)
        {
            var classList = await _classService.GetClassesByStudentId(studentId);
            Classes.Clear();
            foreach (var cls in classList)
            {
                Classes.Add(cls);
                foreach (var assignment in cls.Assignments)
                {
                    Assignments.Add(assignment);
                }
            }
        }

        // Tải bài tập theo ClassID
        public async Task LoadAssignmentsByClassIdAsync(string classId)
        {
            var cls = await _classService.GetClassById(classId);
            if (cls != null) // Kiểm tra xem lớp học có tồn tại không
            {
                Assignments.Clear(); // Xóa các bài tập cũ
                foreach (var assignment in cls.Assignments)
                {
                    Assignments.Add(assignment); // Thêm bài tập vào danh sách
                }
            }
        }

        // PropertyChanged event handler for UI updates
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
