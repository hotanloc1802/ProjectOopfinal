using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels;
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

namespace ClassroomManagementApp1.Views
{
    /// <summary>
    /// Interaction logic for TeacherView.xaml
    /// </summary>
    public partial class AdminClassView : Window
    {
        public AdminClassView()
        {
            InitializeComponent();
            DataContext = new AdminClassViewModel();
        }
        private void Btn_ChangeViewToStudent(object sender, RoutedEventArgs e)
        {
            AdminStudentView Window = new AdminStudentView();
            Window.Show();
            this.Close();
        }
        private void Btn_ChangeViewToteacher(object sender, RoutedEventArgs e)
        {
            AdminTeacherView Window = new AdminTeacherView();
            Window.Show();
            this.Close();
        }

        private void btnAssignment_Click(object sender, RoutedEventArgs e)
        {

        }
        /*private void boxSearch_KeyDown(object sender, KeyEventArgs e)
{
   if (e.Key == Key.Enter)
   {
       //var searchText = boxSearch.Text;
       var command = DataContext?.GetType().GetProperty("SearchCommand")?.GetValue(DataContext) as ICommand;
       if (command != null && command.CanExecute(searchText))
       {
           command.Execute(searchText);
       }
   }
}*/



    }
}