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
namespace ClassroomManagementApp1.Component
{
    /// <summary>
    /// Interaction logic for ClassroomListViewAssignments.xaml
    /// </summary>
    public partial class ClassroomListViewAssignments : UserControl
    {
        public ClassroomListViewAssignments()
        {
            InitializeComponent();
            DataContext = new ClassroomListViewAssignmentViewModel();
        }
    }
}
