using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassroomManagementApp1.ViewModels;
namespace ClassroomManagementApp1.ViewModels.BoxClasses
{
    public class MainWindowBoxAssignmentsItem : ViewModelBase
    {
        public string _subjectname { get; }
        public string _duedate { get; }
        public MainWindowBoxAssignmentsItem(string subjectname, string duedate)
        {
            _subjectname=subjectname;
            _duedate=duedate;
        }
    }

}
