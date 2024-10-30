using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels.BoxClasses;
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

namespace ClassroomManagementApp1.Component
{
    /// <summary>
    /// Interaction logic for MainWindowBoxClasses1.xaml
    /// </summary>
    public partial class MainWindowBoxClasses1 : UserControl
    {
        public MainWindowBoxClasses1()
        {
            InitializeComponent();
            DataContext = new MainWindowBoxClassesViewModel();
        }
        private void BtnClassroom1_Click(object sender, RoutedEventArgs e)
        {
            ClassesView ClassWindow = new ClassesView();
            ClassWindow.Show();
        }
    }
}
