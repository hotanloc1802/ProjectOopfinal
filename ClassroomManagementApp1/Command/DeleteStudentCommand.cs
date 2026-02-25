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
            return true; // Always allow execution
        }

        public void Execute(object parameter)
        {
            // Check if there is an empty row
            var hasEmptyRow = _studentViewModel.Students.Any(s => string.IsNullOrEmpty(s.studentid));

            if (!hasEmptyRow) // If no empty row exists, add a new one
            {
                _studentViewModel.Students.Add(new Student
                {
                    studentid = string.Empty,    // Empty ID
                    studentname = string.Empty,  // Empty name
                    studentemail = string.Empty, // Empty email
                    studentgrade = string.Empty        // Default value
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
