using ClassroomManagement.Application.Services;
using ClassroomManagementApp1.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClassroomManagementApp1.Views
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignInView : Window
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICurrentStudentContext _currentStudentContext;

        public SignInView() : this(
            App.Services.GetRequiredService<IAuthenticationService>(),
            App.Services.GetRequiredService<ICurrentStudentContext>())
        {
        }

        public SignInView(IAuthenticationService authenticationService, ICurrentStudentContext currentStudentContext)
        {
            _authenticationService = authenticationService;
            _currentStudentContext = currentStudentContext;
            InitializeComponent();
        }

        //Hide text in text box
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender == boxUserName)
            {
                txtUserName.Visibility = Visibility.Collapsed;
            }
            else if (sender == boxPassword)
            {
                txtPassword.Visibility = Visibility.Collapsed;
            }
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender == boxUserName && string.IsNullOrWhiteSpace(boxUserName.Text))
            {
                txtUserName.Visibility = Visibility.Visible;
            }
            else if (sender == boxPassword && string.IsNullOrWhiteSpace(boxPassword.Password))
            {
                txtPassword.Visibility = Visibility.Visible;
            }
        }
        private void boxUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                boxPassword.Focus(); // Move focus to PasswordBox
            }
        }

        // Event to handle Enter key for login after entering Password
        private void boxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (!string.IsNullOrEmpty(boxUserName.Text) && !string.IsNullOrEmpty(boxPassword.Password))
                {
                    LogIn_Click(sender, e); // Trigger the login function
                }
            }
        }
        private async void LogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = boxUserName.Text;
                var password = boxPassword.Password;

                var result = await _authenticationService.AuthenticateAsync(username, password);
                if (result == null)
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng.", "Đăng nhập thất bại", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var (studentId, role) = result.Value;
                _currentStudentContext.StudentId = studentId;

                if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    var adminWindow = new AdminStudentView();
                    adminWindow.Show();
                }
                else
                {
                    var mainWindow = ActivatorUtilities.CreateInstance<MainWindowView>(App.Services, studentId);
                    mainWindow.Show();
                }

                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
