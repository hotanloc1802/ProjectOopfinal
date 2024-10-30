// File: Commands/SearchClassCommand.cs
using System;
using System.Windows;
using System.Windows.Input;
using ClassroomManagementApp1.ViewModels;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using ClassroomManagementApp1.Views; // Giả sử `ClassViewModel` nằm trong namespace này

namespace ClassroomManagementApp1.Commands
{
    public class SearchClassCommand : ICommand
    {
        private readonly ClassViewModel _classViewModel;

        public SearchClassCommand(ClassViewModel classViewModel)
        {
            _classViewModel = classViewModel ?? throw new ArgumentNullException(nameof(classViewModel));
        }

         public bool CanExecute(object parameter)
    {
        return parameter is string classId && !string.IsNullOrEmpty(classId);
    }

        public async void Execute(object parameter)
        {
            if (parameter is string classId && !string.IsNullOrEmpty(classId))
            {
                await _classViewModel.LoadClassByIdAsync(classId); // Đợi phương thức hoàn thành

                if (_classViewModel.SelectedClass != null) // Kiểm tra SelectedClass
                {
                    ShowClassInfoWindow(classId);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy lớp học với ID đã nhập.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }


        private void ShowClassInfoWindow(string classID)
        {
            var classInfoWindow = new ClassInfoView()
            {
                DataContext = new ClassInfoViewModel(classID)
            };
            classInfoWindow.Show();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
