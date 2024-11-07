using ClassroomManagementApp1.ViewModels.ComponentViewModel;
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

namespace ClassroomManagementApp1.Component
{
    /// <summary>
    /// Interaction logic for AdminTeacherInfoView.xaml
    /// </summary>
    public partial class AdminTeacherInfoView : UserControl
    {
        public AdminTeacherInfoView()
        {
            InitializeComponent();
            DataContext = new AdminTeacherInfoViewModel();
        }
        private void teacherDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Selection Changed!");
        }
    }    
}
