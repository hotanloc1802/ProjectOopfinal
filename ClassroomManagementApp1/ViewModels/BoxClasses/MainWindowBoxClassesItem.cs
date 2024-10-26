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
        public string _teacherid { get; }
        public string _classid { get; }
        public string _datebegin { get; }
        public string _dateend { get; }
        public MainWindowBoxClassesItem(string teacherid, string classid, string datebegin, string dateend)
        {
            _teacherid = teacherid;
            _classid = classid;
            _datebegin = datebegin;
            _dateend = dateend;
        }
    }

}
