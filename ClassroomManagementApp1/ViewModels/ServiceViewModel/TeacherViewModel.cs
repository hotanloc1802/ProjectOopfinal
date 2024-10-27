using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomManagementApp1.ViewModels.ServiceViewModels
{
    public class TeacherViewModel : INotifyPropertyChanged
    {
        private readonly TeacherService _teacherService;

        public ObservableCollection<Teacher> Teachers { get; set; }


        // Thuộc tính để hiển thị thông tin giáo viên đang chọn
        private Teacher _selectedTeacher;
        public Teacher SelectedTeacher
        {
            get => _selectedTeacher;
            set
            {
                _selectedTeacher = value;
                OnPropertyChanged(nameof(SelectedTeacher));
            }
        }

        public TeacherViewModel(TeacherService teacherService)
        {
            _teacherService = teacherService;
            Teachers = new ObservableCollection<Teacher>();
            LoadTeachers();  // Tải danh sách giáo viên ban đầu
        }

        // Phương thức tải tất cả giáo viên
        private async void LoadTeachers()
        {
            var teacherList = await _teacherService.GetAllTeachersAsync();
            Teachers.Clear();
            foreach (var teacher in teacherList)
            {
                Teachers.Add(teacher);
            }
        }

        // Thêm giáo viên mới
        public async Task AddTeacher(Teacher teacher)
        {
            await _teacherService.AddTeacherAsync(teacher);
            LoadTeachers();  // Tải lại danh sách giáo viên sau khi thêm
        }

        // Cập nhật thông tin giáo viên
        public async Task UpdateTeacher()
        {
            if (SelectedTeacher != null)
            {
                await _teacherService.UpdateTeacherAsync(SelectedTeacher);
            }
            LoadTeachers();  // Tải lại danh sách giáo viên sau khi cập nhật
        }

        // Xóa giáo viên
        public async Task DeleteTeacher()
        {
            if (SelectedTeacher != null)
            {
                await _teacherService.DeleteTeacherAsync(SelectedTeacher.teacherid);
                Teachers.Remove(SelectedTeacher);
                SelectedTeacher = null;
            }
            LoadTeachers();  // Tải lại danh sách giáo viên sau khi xóa
        }
        public async Task<string> GetTeacherInfo(string teacherid)
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            var teacher = teachers.FirstOrDefault(s => s.teacherid == teacherid);

            if (teacher != null)
            {
                return $"Teacher ID: {teacher.teacherid}";
            }
            return "Teacher not found.";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
