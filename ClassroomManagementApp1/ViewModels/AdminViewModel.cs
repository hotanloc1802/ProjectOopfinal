using ClassroomManagementApp1.Commands;
using ClassroomManagementApp1.Component;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.Views;  // Giả sử bạn có các View này
using System;
using System.Windows.Input;

namespace ClassroomManagementApp1.ViewModels
{
    public class AdminViewModel : ViewModelBase
    {
        private object _selectedView;
        public object SelectedView
        {
            get { return _selectedView; }
            set
            {
                _selectedView = value;
                OnPropertyChanged(nameof(SelectedView));  // Thông báo sự thay đổi cho UI
            }
        }

        // ICommand để binding với button
        public ICommand ChangeViewToTeachersCommand { get; private set; }
        public ICommand ChangeViewToStudentsCommand { get; private set; }
        public ICommand ChangeViewToClassesCommand { get; private set; }

        public AdminViewModel()
        {
            // Mặc định hiển thị AdminStudentInfoView
            SelectedView = new AdminStudentInfoView(); // View mặc định hiển thị

            // Khởi tạo ICommand với các phương thức thay đổi view
            ChangeViewToTeachersCommand = new RelayCommand<object>(ChangeViewToTeachers);
            ChangeViewToStudentsCommand = new RelayCommand<object>(ChangeViewToStudents);
            ChangeViewToClassesCommand = new RelayCommand<object>(ChangeViewToClasses);
        }

        // Hàm thay đổi view khi button "Teachers" được nhấn
        private void ChangeViewToTeachers(object obj)
        {
            SelectedView = new AdminTeacherInfoView(); // Thay đổi view thành thông tin giáo viên
        }

        // Hàm thay đổi view khi button "Students" được nhấn
        private void ChangeViewToStudents(object obj)
        {
            SelectedView = new AdminStudentInfoView(); // Thay đổi view thành thông tin học sinh
        }
        private void ChangeViewToClasses(object obj)
        {
            SelectedView = new AdminClassInfoView();
        }
    }
}
