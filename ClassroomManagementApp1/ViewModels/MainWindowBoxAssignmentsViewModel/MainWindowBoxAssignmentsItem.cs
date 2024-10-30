using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ClassroomManagementApp1.ViewModels.MainWindowBoxAssignmentsViewModel
{
    public class MainWindowBoxAssignmentsItem : ViewModelBase
    {
        public string _description { get; }
        public string _duedate { get; }
        public MainWindowBoxAssignmentsItem(string description, string duedate)
        {
            _description = description;
            _duedate = duedate;
        }
    }

}
