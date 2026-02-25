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
                    await _classViewModel.LoadClassByIdAsync(classId); // Wait for the method to complete

                    if (_classViewModel.SelectedClass != null) // Check SelectedClass
                    {
                        ShowClassInfoWindow(classId);
                    }
                    else
                    {
                        MessageBox.Show("Class with the entered ID was not found.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid class ID.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
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
