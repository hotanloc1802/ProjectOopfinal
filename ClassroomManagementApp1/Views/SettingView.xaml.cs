using ClassroomManagementApp1.Views;
using MahApps.Metro.IconPacks;
using Microsoft.Extensions.DependencyInjection;
using ClassroomManagementApp1.Services;
using Microsoft.Win32;
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
namespace ClassroomManagementApp1.Views
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class SettingView : Window
    {
        public SettingView()
        {
            InitializeComponent();
            var studentId = App.Services.GetRequiredService<ICurrentStudentContext>().StudentId;
            if (string.IsNullOrWhiteSpace(studentId))
            {
                new SignInView().Show();
                Close();
                return;
            }

            DataContext = ActivatorUtilities.CreateInstance<SettingViewModel>(App.Services, studentId);
        }
        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            var studentId = App.Services.GetRequiredService<ICurrentStudentContext>().StudentId;
            if (string.IsNullOrWhiteSpace(studentId))
            {
                new SignInView().Show();
                Close();
                return;
            }

            MainWindowView DashboardWindow = new MainWindowView(studentId);
            DashboardWindow.Show();
            this.Close();
        }
        private void BtnClassroom_Click(object sender, RoutedEventArgs e)
        {
            ClassroomsView ClassroomWindow = new ClassroomsView();
            ClassroomWindow.Show();
            this.Close();
        }
        private void BtnClassroom1_Click(object sender, RoutedEventArgs e)
        {
            var studentId = App.Services.GetRequiredService<ICurrentStudentContext>().StudentId;
            if (string.IsNullOrWhiteSpace(studentId))
            {
                new SignInView().Show();
                Close();
                return;
            }

            ClassesView ClassWindow = new ClassesView(studentId);
            ClassWindow.Show();
            this.Close();
        }
        private void BtnAssignment_Click(object sender, RoutedEventArgs e)
        {
            AssignmentsView AssignmentWindow = new AssignmentsView();
            AssignmentWindow.Show();
            this.Close();
        }
        private void Setting_Click(object obj, RoutedEventArgs e)
        {
            SettingView SettingWindow = new SettingView();
            SettingWindow.Show();
            this.Close();
        }
        private void SignOut_Click(object obj, RoutedEventArgs e)
        {
            SignInView SignInWindow = new SignInView();
            SignInWindow.Show();
            this.Close();
        }
        private void ChangeInfo_Click(object obj, RoutedEventArgs e)
        {
            ChangeInfoView ChangeInfoWindow = new ChangeInfoView();
            ChangeInfoWindow.Show();
        }
        private void ChangePass_Click(object obj, RoutedEventArgs e)
        {
            ChangePassView ChangePassWindow = new ChangePassView();
            ChangePassWindow.Show();
        }
        public void RefreshWindow()
        {
            // Code to refresh the window, such as reloading data or resetting controls.
            // Example: Reload data for a ListView or re-bind collections.
            DataContext = null;  // Reset the data context if necessary
            DataContext = this;  // Reassign the data context or refresh data here
        }

        //Change Password
        private bool isEditingPassword = false;
       /* private void btnChangeSave_Click(object sender, RoutedEventArgs e)
        {
            if (isEditingPassword)
            {
                // Đang ở chế độ chỉnh sửa, nhấn "Save"
                isEditingPassword = false;
                txtPasswordDots.Visibility = Visibility.Visible;
                boxPassword.Visibility = Visibility.Collapsed;

                // Đổi icon về Pencil (cây bút)
                iconChangeSave.Kind = PackIconMaterialKind.Pencil;
            }
            else
            {
                // Đang ở chế độ xem, nhấn "Change"
                isEditingPassword = true;
                txtPasswordDots.Visibility = Visibility.Collapsed;
                boxPassword.Visibility = Visibility.Visible;
                boxPassword.Focus();

                // Đổi icon về ContentSave (Lưu)
                iconChangeSave.Kind = PackIconMaterialKind.ContentSave;
            }
        }
        private void SignOut_Click(object obj, RoutedEventArgs e)
        {
            SignInView SignInWindow = new SignInView();
            SignInWindow.Show();
            this.Close();
        }
        //Change profile pic
        private void ChangeProfilePicture_Click(object sender, RoutedEventArgs e)
        {
            // Mở hộp thoại chọn file
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg",
                Title = "Select Profile Picture"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Tải ảnh đã chọn và gán vào Image
                string selectedImagePath = openFileDialog.FileName;
                imgProfilePicture.Source = new BitmapImage(new Uri(selectedImagePath));

                // Bạn có thể thêm code lưu đường dẫn của ảnh nếu muốn lưu ảnh được chọn
            }
        }*/
    }
}
