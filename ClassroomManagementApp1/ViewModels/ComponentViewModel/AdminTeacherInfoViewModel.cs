using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomManagementApp1.ViewModels.ComponentViewModel
{
    class AdminTeacherInfoViewModel : ViewModelBase
    {
        private ObservableCollection<TeacherItem> _teachers;
        public ObservableCollection<TeacherItem> Teachers
        {
            get { return _teachers; }
            set
            {
                _teachers = value;
                OnPropertyChanged(nameof(Teachers));
            }
        }

        public AdminTeacherInfoViewModel()
        {
            // Khởi tạo với dữ liệu mẫu
            Teachers = new ObservableCollection<TeacherItem>
            {
                new TeacherItem("S001", "John Doe", "johndoe@example.com"),
                new TeacherItem("S002", "Jane Smith", "janesmith@example.com"),
                new TeacherItem("S003", "Michael Brown", "michaelbrown@example.com")
            };
        }

        public class TeacherItem
        {
            public string TeacherId { get; set; }
            public string TeacherName { get; set; }
            public string Email { get; set; }

            public TeacherItem() { }

            public TeacherItem(string studentId, string studentName, string email)
            {
                TeacherId = studentId;
                TeacherName = studentName;
                Email = email;
            }
        }
    }
}