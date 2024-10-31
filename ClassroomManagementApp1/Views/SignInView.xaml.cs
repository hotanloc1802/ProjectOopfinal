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

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=public";

            // SQL query to check the user and get the studentId
            string query = "SELECT studentid FROM \"public\".account WHERE username = @Username AND password = @Password";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", boxUserName.Text);
                        command.Parameters.AddWithValue("@Password", boxPassword.Password);

                        // Execute the query and retrieve studentId (if found)
                        var result = command.ExecuteScalar();

                        if (result != null) // User found, studentId retrieved
                        {
                            string studentId = result.ToString();
                            StudentContext.Instance.SetStudentId(studentId);
                            // Pass the studentId to MainWindow
                            MainWindowView mainWindow = new MainWindowView(studentId);
                            mainWindow.Show();
                            this.Hide(); // Hide the login form
                        }
                        else
                        {
                            MessageBox.Show("Tài khoản hoặc mật khẩu không đúng.", "Đăng nhập thất bại", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }
    }
}
