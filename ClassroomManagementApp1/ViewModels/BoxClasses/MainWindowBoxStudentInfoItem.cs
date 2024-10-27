using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassroomManagementApp1.ViewModels;
namespace ClassroomManagementApp1.ViewModels.BoxClasses
{
    public class MainWindowBoxStudentInfoItem : ViewModelBase
    {
        public string _studentname { get; }
        public string _studentimage { get; }
        public MainWindowBoxStudentInfoItem(string studentname, string studentimage)
        {
            _studentname = studentname;
            _studentimage = studentimage;
        }
    }

}
