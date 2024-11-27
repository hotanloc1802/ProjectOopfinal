using System;
using System.Linq;
using System.Windows.Input;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;

namespace ClassroomManagementApp1.Commands
{
    public class AddStudentCommand : ICommand
    {
        private readonly StudentViewModel _studentViewModel;

        public AddStudentCommand(StudentViewModel studentViewModel)
        {
            _studentViewModel = studentViewModel ?? throw new ArgumentNullException(nameof(studentViewModel));
        }

        public bool CanExecute(object parameter)
        {
            return true; // Luôn cho phép thực thi
        }

        public void Execute(object parameter)
        {
            // Kiểm tra xem đã có dòng trống hay chưa
            var hasEmptyRow = _studentViewModel.Students.Any(s => string.IsNullOrEmpty(s.studentid));

            if (!hasEmptyRow) // Nếu chưa có dòng trống, thêm dòng mới
            {
                _studentViewModel.Students.Add(new Student
                {
                    studentid = string.Empty,    // ID trống
                    studentname = string.Empty,  // Tên trống
                    studentemail = string.Empty, // Email trống
                    studentgrade = string.Empty        // Giá trị mặc định
                });
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
