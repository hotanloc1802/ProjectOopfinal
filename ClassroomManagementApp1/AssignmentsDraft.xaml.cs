using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Views;
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

namespace ClassroomManagementApp1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AssignmentsDraft : Window
    {
        public AssignmentsDraft()
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
    }

}
