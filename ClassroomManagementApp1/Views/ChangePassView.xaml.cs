using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.ViewModels;
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
using System.Xml.Linq;

namespace ClassroomManagementApp1.Views
{
    /// <summary>
    /// Interaction logic for ChangePassView.xaml
    /// </summary>
    public partial class ChangePassView : Window
    {
        public ChangePassView()
        {
            InitializeComponent();
            DataContext = new ChangeViewModel(StudentContext.Instance.StudentId, this);
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Kiểm tra kết quả chọn của người dùng
            if (result == MessageBoxResult.Yes)
            {
                this.Close(); // Đóng cửa sổ nếu chọn Yes
            }
            // Nếu chọn No, không làm gì cả
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void TextBox_GotFocusPass(object sender, RoutedEventArgs e)
        {
            txtPass.Visibility = Visibility.Collapsed;
        }

        private void TextBox_LostFocusPass(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(boxPass.Text))
            {
                txtPass.Visibility = Visibility.Visible;
            }
            else
            {
                txtPass.Visibility = Visibility.Collapsed;
            }
        }
        private void TextBox_GotFocusConfirm(object sender, RoutedEventArgs e)
        {
            txtConfirm.Visibility = Visibility.Collapsed;
        }

        private void TextBox_LostFocusConfirm(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(boxConfirm.Text))
            {
                txtConfirm.Visibility = Visibility.Visible;
            }
            else
            {
                txtConfirm.Visibility = Visibility.Collapsed;
            }
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Update the ViewModel when the password changes
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                // Access the ViewModel via DataContext
                var viewModel = this.DataContext as ChangeViewModel;
                if (viewModel != null)
                {
                    // Update the PassWord property
                    viewModel.PassWord = passwordBox.Password;
                }
            }
        }
    }
}
