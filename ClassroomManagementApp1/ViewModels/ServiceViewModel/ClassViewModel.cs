using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ClassroomManagementApp1.ViewModels.ServiceViewModels
{
    public class ClassViewModel : INotifyPropertyChanged
    {
        private readonly ClassesService _classService;

        // ObservableCollection to notify UI of changes
        public ObservableCollection<Class> Classes { get; set; } = new ObservableCollection<Class>();

        public ClassViewModel(ClassesService classService)
        {
            _classService = classService;
        }
        public Class SelectedClass { get; set; }
        public Class _selectedClass
        {
            get => _selectedClass;
            set
            {
                _selectedClass = value;

            }
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
            SelectedClass = cls; // Gán lớp học vào property để hiển thị
        }
        public async Task LoadClassesByStudentIdAsync(string studentId)
        {
            var classList = await _classService.GetClassesByStudentId(studentId);
            Classes.Clear();
            foreach (var cls in classList)
            {
                Classes.Add(cls);
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
