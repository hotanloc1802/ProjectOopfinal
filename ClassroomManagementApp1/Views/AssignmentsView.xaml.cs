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
using Microsoft.Win32;
using System.IO;
using ClassroomManagementApp1.Views;
using ClassroomManagementApp1.Data;


namespace ClassroomManagementApp1.Views
{
    /// <summary>
    /// Interaction logic for Assignments.xaml
    /// </summary>
    public partial class AssignmentsView : Window
    {
        public AssignmentsView()
        {
            InitializeComponent();
        }
        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            MainWindowView DashboardWindow = new MainWindowView(StudentContext.Instance.StudentId);
            DashboardWindow.Show();
            this.Close();
        }
        private void BtnClassroom_Click(object sender, RoutedEventArgs e)
        {
            ClassroomsView ClassroomWindow = new ClassroomsView();
            ClassroomWindow.Show();
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
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*|PDF Files (*.pdf)|*.pdf|Word Documents (*.docx)|*.docx";

            // Hiển thị hộp thoại cho người dùng và kiểm tra xem họ đã chọn tệp hay chưa
            if (openFileDialog.ShowDialog() == true)
            {
                // Lấy đường dẫn tệp được chọn
                string selectedFilePath = openFileDialog.FileName;

                // Tại đây, bạn có thể thực hiện các thao tác với tệp, ví dụ: lưu, gửi tệp, v.v.
                MessageBox.Show("Chosen file: " + selectedFilePath);
            }
        }

    }
}
