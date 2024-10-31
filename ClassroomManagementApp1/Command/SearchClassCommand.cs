using System;
using System.Windows;
using System.Windows.Input;
using ClassroomManagementApp1.ViewModels;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using ClassroomManagementApp1.Views; 
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
                try
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
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("ID lớp học không hợp lệ.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
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
