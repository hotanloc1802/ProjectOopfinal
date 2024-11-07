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
using ClassroomManagementApp1.ViewModels.ComponentViewModel;

namespace ClassroomManagementApp1.Component
{
    /// <summary>
    /// Interaction logic for AdminClassInfoView.xaml
    /// </summary>
    public partial class AdminClassInfoView : UserControl
    {
        public AdminClassInfoView()
        {
            InitializeComponent();
            DataContext = new AdminClassInfoViewModel();
        }
        private void classDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Selection Changed!");
        }
    }
}
