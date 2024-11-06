using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Commands;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using ClassroomManagementApp1.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClassroomManagementApp1.ViewModels
{
    public class ChangeViewModel : ViewModelBase
    {
        public StudentViewModel StudentViewModel { get; private set; }
        public AccountViewModel AccountViewModel { get; private set; }

        private readonly AccountService _accountService;
        private readonly StudentService _studentService;
        private readonly Window _window;  // Window reference

        public ICommand ChangeAccountCommand { get; private set; }
        public ICommand ChangePassCommand { get; private set; }
        public ChangeViewModel(string studentId, Window window)
            : this(CreateDbContext().Item1, CreateDbContext().Item2, studentId, window)
        {
        }

        public ChangeViewModel(AccountService accountService, StudentService studentService, string studentId, Window window)
        {
            _accountService = accountService;
            _studentService = studentService;
            _window = window;  // Store window reference

            AccountViewModel = new AccountViewModel(_accountService);
            StudentViewModel = new StudentViewModel(_studentService);

            LoadAccountInformation(studentId).ConfigureAwait(false);

            // Initialize the command with the action and a condition for execution
            ChangeAccountCommand = new RelayCommand(async _ => await ChangeAccount(studentId));
            ChangePassCommand = new RelayCommand(async _ => await ChangePass(studentId));
        }

        private static (AccountService, StudentService) CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=OOP-new,public;");
            var context = new AppDbContext(optionsBuilder.Options);

            var accountService = new AccountService(context);
            var studentService = new StudentService(context);

            return (accountService, studentService);
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
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (checkpass != PassWord)
            {
                MessageBox.Show("Sai mật khẩu, vui lòng nhập lại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    // Check if the new information is valid
                    if (string.IsNullOrEmpty(StudentName) || string.IsNullOrEmpty(StudentBirth))
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Update the student information
                    var student = await _studentService.GetStudentById(studentId);
                    if (student != null)
                    {
                        student.studentname = StudentName;
                        student.studentbirth = StudentBirth;

                        await _studentService.UpdateStudentAsync(student);
                        MessageBox.Show("Thông tin sinh viên đã được cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                        // Close the window after update
                        _window.Close();  // Close the window
                        SettingView newSettingView = new SettingView();
                        //var newViewModel = new SettingViewModel(StudentContext.Instance.StudentId);
                        //newSettingView.DataContext = newViewModel;
                        newSettingView.Show();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin sinh viên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private async Task ChangePass(string studentId)
        {
            await AccountViewModel.LoadAccountByStudentIDAsync(studentId);
            var checkpass = AccountViewModel.SelectedAccount.password;
            if ( ResetPassword1 == null || ResetPassword2 == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (ResetPassword1 != ResetPassword2)
            {
                MessageBox.Show("Vui lòng xác nhận lại mật khẩu mới!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if(checkpass != PassWord)
            {
                MessageBox.Show("Sai mật khẩu, vui lòng nhập lại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                var account = await _accountService.GetAccountByStudentID(studentId);
                if (account != null)
                {
                    account.password = ResetPassword1;
                    await _accountService.UpdateAccountAsync(account);
                }
                MessageBox.Show("Thông tin mật khẩu đã được cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                // Close the window after update
                _window.Close();  // Close the window
                SettingView newSettingView = new SettingView();
                //var newViewModel = new SettingViewModel(StudentContext.Instance.StudentId);
                //newSettingView.DataContext = newViewModel;
                newSettingView.Show();
            }
        }
    }
}
