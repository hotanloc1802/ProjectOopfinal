using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassroomManagementApp1.ViewModels;
namespace ClassroomManagementApp1.ViewModels.BoxClasses
{
    public class MainWindowBoxClassesItem : ViewModelBase
    {
        public string _classname { get; }
        public string _teachername { get; }
        public int _assignmentcount{ get; }
        public string _datebegin { get; }
        public string _dateend { get; }

        public MainWindowBoxClassesItem(string classname, string teachername, int assignmentcount, string datebegin ,string dateend)
        {
            _classname = classname;
            _teachername = teachername;
            _assignmentcount = assignmentcount;
            _datebegin = datebegin;
            _dateend = dateend;
        }
    }

}
