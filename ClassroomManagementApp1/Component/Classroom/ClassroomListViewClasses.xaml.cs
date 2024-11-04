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
using ClassroomManagementApp1.ViewModels.ComponentViewModel;
using ClassroomManagementApp1.Views;
namespace ClassroomManagementApp1.Component
{
    /// <summary>
    /// Interaction logic for ClassroomListViewClasses.xaml
    /// </summary>
    public partial class ClassroomListViewClasses : UserControl
    {
        public ClassroomListViewClasses()
        {
            InitializeComponent();
            DataContext = new ClassroomListViewClassesViewModel();
        }
        private void BtnClassroom1_Click(object sender, RoutedEventArgs e)
        {
            // Lấy classid từ CommandParameter
            if (sender is Button button && button.CommandParameter is string classid)
            {
                ClassesView classWindow = new ClassesView(classid);
                classWindow.Show();

                // Ẩn cửa sổ cha nếu tồn tại
                Window parentWindow = Window.GetWindow(this);
                if (parentWindow != null)
                {
                    parentWindow.Visibility = Visibility.Hidden;
                }
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
