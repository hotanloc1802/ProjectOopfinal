using ClassroomManagementApp1.Commands;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using ClassroomManagementApp1.Views;
using ClassroomManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using System.Windows;

namespace ClassroomManagementApp1.ViewModels
{
    public class ChangeViewModel : ViewModelBase
    {
        public StudentViewModel StudentViewModel { get; private set; }
        public AccountViewModel AccountViewModel { get; private set; }

        private readonly IAccountService _accountService;
        private readonly IStudentService _studentService;
        private readonly Window _window;  // Window reference

        public ICommand ChangeAccountCommand { get; private set; }
        public ICommand ChangePassCommand { get; private set; }

        public ChangeViewModel(string studentId, Window window)
            : this(
                App.Services.GetRequiredService<IAccountService>(),
                App.Services.GetRequiredService<IStudentService>(),
                studentId,
                window)
        {
        }

        public ChangeViewModel(IAccountService accountService, IStudentService studentService, string studentId, Window window)
        {
            _accountService = accountService;
            _studentService = studentService;
            _window = window;  // Store window reference

            AccountViewModel = new AccountViewModel(_accountService);
            StudentViewModel = new StudentViewModel(_studentService);

            LoadAccountInformation(studentId).ConfigureAwait(false);

            // Initialize commands
            ChangeAccountCommand = new RelayCommand(async _ => await ChangeAccount(studentId));
            ChangePassCommand = new RelayCommand(async _ => await ChangePass(studentId));
        }

        private string studentname;
        public string StudentName
        {
            get => studentname;
            set
            {
                studentname = value;
                OnPropertyChanged(nameof(StudentName));
            }
        }

        private string studentbirth;
        public string StudentBirth
        {
            get => studentbirth;
            set
            {
                studentbirth = value;
                OnPropertyChanged(nameof(StudentBirth));
            }
        }

        private string password;
        public string PassWord
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(PassWord));
            }
        }

        private string resetpassword1;
        public string ResetPassword1
        {
            get => resetpassword1;
            set
            {
                resetpassword1 = value;
                OnPropertyChanged(nameof(ResetPassword1));
            }
        }

        private string resetpassword2;
        public string ResetPassword2
        {
            get => resetpassword2;
            set
            {
                resetpassword2 = value;
                OnPropertyChanged(nameof(ResetPassword2));
            }
        }

        // Load the current account information for the specified student ID
        private async Task LoadAccountInformation(string studentId)
        {
            await StudentViewModel.LoadStudentByIdAsync(studentId);
            var studentInfo = StudentViewModel.SelectedStudent;
            if (studentInfo != null)
            {
                StudentName = studentInfo.studentname;
                StudentBirth = studentInfo.studentbirth;
            }
        }

        // Command method to update account information in the database
        private async Task ChangeAccount(string studentId)
        {
            await AccountViewModel.LoadAccountByStudentIDAsync(studentId);
            var checkpass = AccountViewModel.SelectedAccount.password;
            if (checkpass == null)
            {
                MessageBox.Show("Please fill in all the required information!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (checkpass != PassWord)
            {
                MessageBox.Show("Incorrect password, please try again.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    if (string.IsNullOrEmpty(StudentName) || string.IsNullOrEmpty(StudentBirth))
                    {
                        MessageBox.Show("Please fill in all the required information!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var student = await _studentService.GetStudentById(studentId);
                    if (student != null)
                    {
                        student.studentname = StudentName;
                        student.studentbirth = StudentBirth;

                        await _studentService.UpdateStudentAsync(student);
                        MessageBox.Show("Student information has been updated!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);

                        _window.Close();  // Close the window
                        new SettingView().Show();
                    }
                    else
                    {
                        MessageBox.Show("Student information not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Command method to update the password in the database
        private async Task ChangePass(string studentId)
        {
            await AccountViewModel.LoadAccountByStudentIDAsync(studentId);
            var checkpass = AccountViewModel.SelectedAccount.password;
            if (string.IsNullOrEmpty(ResetPassword1) || string.IsNullOrEmpty(ResetPassword2))
            {
                MessageBox.Show("Please fill in all the required information!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (ResetPassword1 != ResetPassword2)
            {
                MessageBox.Show("Please confirm the new password again!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (checkpass != PassWord)
            {
                MessageBox.Show("Incorrect password, please try again.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var account = await _accountService.GetAccountByStudentID(studentId);
                if (account != null)
                {
                    account.password = ResetPassword1;
                    await _accountService.UpdateAccountAsync(account);
                    MessageBox.Show("Password has been updated!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);

                    _window.Close();  // Close the window
                    new SettingView().Show();
                }
            }
        }
    }
}
