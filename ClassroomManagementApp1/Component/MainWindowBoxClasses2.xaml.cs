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
    /// Interaction logic for MainWindowBoxClasses2.xaml
    /// </summary>
    public partial class MainWindowBoxClasses2 : UserControl
    {
        public MainWindowBoxClasses2()
        {
            InitializeComponent();
            DataContext = new MainWindowBoxClasses2ViewModel();
        }
        private void BtnClassroom1_Click(object sender, RoutedEventArgs e)
        {
            ClassesView ClassWindow = new ClassesView();
            ClassWindow.Show();
        }
    }
}
