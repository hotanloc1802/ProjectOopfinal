using ClassroomManagementApp1;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.DesignPattern;
namespace ClassroomManagementApp1.Views
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignInView : Window
    {
        public SignInView()
        {
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
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=public";

            // SQL query to check the user and get the studentId and role
            string query = "SELECT studentid, role FROM \"public\".account WHERE username = @Username AND password = @Password";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", boxUserName.Text);
                        command.Parameters.AddWithValue("@Password", boxPassword.Password);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // User found
                            {
                                string studentId = reader["studentid"].ToString();
                                string role = reader["role"].ToString();

                                // Set the studentId in context
                                StudentContextSingleton.Instance.SetStudentId(studentId);

                                if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Open Admin interface
                                    AdminStudentView adminWindow = new AdminStudentView();
                                    adminWindow.Show();
                                }
                                else
                                {
                                    // Open Normal interface
                                    MainWindowView mainWindow = new MainWindowView(studentId);
                                    mainWindow.Show();
                                }

                                this.Hide(); // Hide the login form
                            }
                            else
                            {
                                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng.", "Đăng nhập thất bại", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
