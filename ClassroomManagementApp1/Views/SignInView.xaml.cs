using ClassroomManagementApp1;
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
            string username = boxUserName.Text;
            string password = boxPassword.Password;

            // Check username and password
            if (username == "Username" && password == "Password") //này là tao giả sử
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("User name or password is incorrect", "Login Fail", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
