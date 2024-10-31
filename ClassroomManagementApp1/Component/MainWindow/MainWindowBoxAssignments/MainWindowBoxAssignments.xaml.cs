using ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxAssignmentsViewModel;
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
    /// Interaction logic for MainWindowBoxAssignments.xaml
    /// </summary>
    public partial class MainWindowBoxAssignments : UserControl
    {
        public MainWindowBoxAssignments()
        {
            InitializeComponent();
            DataContext = new MainWindowBoxAssignmentsViewModel();
        }
    }
}
