using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassroomManagementApp1.Data;
namespace ClassroomManagementApp1.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView(string studentid)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(studentid);
        }
        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            MainWindowView dashboardWindow = new MainWindowView(StudentContext.Instance.StudentId);
            dashboardWindow.Show();
            this.Close();
        }
        private void BtnClassroom_Click(object sender, RoutedEventArgs e)
        {
            ClassroomsView ClassroomWindow = new ClassroomsView();
            ClassroomWindow.Show();
            this.Close();
        }
        private void BtnClass_Click(object sender, RoutedEventArgs e)
        {
            ClassesView ClassWindow = new ClassesView();
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
        //Hide text in text box
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            txtSearch.Visibility = Visibility.Collapsed;
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            txtSearch.Visibility = Visibility.Visible;
        }
        private void boxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var searchText = boxSearch.Text; // Lấy văn bản từ TextBox
                var command = DataContext?.GetType().GetProperty("SearchCommand")?.GetValue(DataContext) as ICommand; // Lấy command từ DataContext
                if (command != null && command.CanExecute(searchText)) // Kiểm tra điều kiện thực thi
                {
                    command.Execute(searchText); // Thực hiện command
                }
            }
        }

    }
}
