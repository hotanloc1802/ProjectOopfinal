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

namespace ClassroomManagementApp1.Views
{
    /// <summary>
    /// Interaction logic for ClassInfo.xaml
    /// </summary>
    public partial class ClassInfoView : Window
    {
        public event EventHandler RequestClose;
        public ClassInfoView()
        {
            InitializeComponent();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
