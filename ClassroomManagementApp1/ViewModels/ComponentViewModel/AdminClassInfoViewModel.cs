using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomManagementApp1.ViewModels.ComponentViewModel
{
    class AdminClassInfoViewModel : ViewModelBase
    {
        private ObservableCollection<ClassItem> _classes;
        public ObservableCollection<ClassItem> Classes
        {
            get { return _classes; }
            set
            {
                _classes = value;
                OnPropertyChanged(nameof(Classes));
            }
        }

        public AdminClassInfoViewModel()
        {
            // Khởi tạo với dữ liệu mẫu
            Classes = new ObservableCollection<ClassItem>
            {
                new ClassItem("S001", "English", "T001", "2024-09-01", "2024-12-01"),
                new ClassItem("S002", "Math", "T002", "2024-10-01", "2025-01-01"),
                new ClassItem("S003", "History", "T003", "2024-11-01", "2025-02-01")
            };
        }

        public class ClassItem
        {
            public string ClassId { get; set; }
            public string ClassName { get; set; }
            public string TeacherId { get; set; }
            public string DateBegin { get; set; }
            public string DateEnd { get; set; }


            public ClassItem() { }

            public ClassItem(string studentId, string studentName, string teacherId, string dateBegin, string dateEnd)
            {
                ClassId = studentId;
                ClassName = studentName;
                TeacherId = teacherId;
                DateBegin = dateBegin;
                DateEnd = dateEnd;
            }
        }
    }
}