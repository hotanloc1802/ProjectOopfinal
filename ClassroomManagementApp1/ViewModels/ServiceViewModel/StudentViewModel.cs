using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace ClassroomManagementApp1.ViewModels.ServiceViewModels
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        private readonly StudentService _studentService;
        public ObservableCollection<Student> Students { get; set; }

        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        public StudentViewModel(StudentService studentService)
        {
            _studentService = studentService;
        }
        // Tải tất cả học sinh
        public async Task LoadAllStudentsAsync()
        {
            var studentList = await _studentService.GetAllStudentsAsync();
            Students.Clear();
            foreach (var student in studentList)
            {
                Students.Add(student);
            }
        }
        // Thêm học sinh mới
        public async Task AddStudentAsync(Student student)
        {
            await _studentService.AddStudentAsync(student);
            await LoadAllStudentsAsync(); // Cập nhật danh sách sau khi thêm
        }
        // Cập nhật thông tin học sinh
        public async Task UpdateStudentAsync(Student student)
        {
            await _studentService.UpdateStudentAsync(student);
            await LoadAllStudentsAsync(); // Cập nhật danh sách sau khi sửa
        }

        // Xóa học sinh
        public async Task DeleteStudentAsync(string studentId)
        {
            await _studentService.DeleteStudentAsync(studentId);
            await LoadAllStudentsAsync(); // Cập nhật danh sách sau khi xóa
        }
        // Tải thông tin học sinh theo ID
        public async Task LoadStudentByIdAsync(string studentId)
        {
            var student = await _studentService.GetStudentById(studentId);
            SelectedStudent = student; // Gán vào SelectedStudent để hiển thị chi tiết
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
