using System;
using System.Windows;
using System.Windows.Input;

namespace ClassroomManagementApp1.Commands // Tạo namespace cho RelayCommand
{
    // Định nghĩa RelayCommand có kiểu generic để làm việc với các tham số kiểu object
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute; // Thay đổi Action thành Action<T> để nhận tham số kiểu T
        private readonly Predicate<T> _canExecute; // Thay đổi Predicate thành Predicate<T>

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            // Kiểm tra nếu canExecute có giá trị, thì gọi canExecute với parameter đã chuyển đổi kiểu
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            // Gọi _execute với parameter đã chuyển đổi kiểu
            _execute((T)parameter);
        }
    }
}