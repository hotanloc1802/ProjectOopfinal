using ClassroomManagementApp1.Data;
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
using ClassroomManagementApp1.ViewModels;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
namespace ClassroomManagementApp1.Views
{
    /// <summary>
    /// Interaction logic for ChangeInfoView.xaml
    /// </summary>
    public partial class ChangeInfoView : Window
    {
        public ChangeInfoView()
        {
            InitializeComponent();
            DataContext = new ChangeViewModel(StudentContext.Instance.StudentId,this);
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
        private void TextBox_GotFocusName(object sender, RoutedEventArgs e)
        {
            txtName.Visibility = Visibility.Collapsed;
        }

        private void TextBox_LostFocusName(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(boxName.Text))
            {
                txtName.Visibility = Visibility.Visible;
            }
            else
            {
                txtName.Visibility = Visibility.Collapsed;
            }
        }
        private void TextBox_GotFocusBirth(object sender, RoutedEventArgs e)
        {
            txtBirth.Visibility = Visibility.Collapsed;
        }

        private void TextBox_LostFocusBirth(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(boxName.Text))
            {
                txtBirth.Visibility = Visibility.Visible;
            }
            else
            {
                txtBirth.Visibility = Visibility.Collapsed;
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