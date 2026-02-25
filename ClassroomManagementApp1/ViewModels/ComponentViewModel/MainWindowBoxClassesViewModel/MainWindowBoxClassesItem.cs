namespace ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxClassesViewModel
{
    public class MainWindowBoxClassesItem : ViewModelBase
    {
        // Class name
        public string _classname { get; }

        // Teacher name
        public string _teachername { get; }

        // Number of assignments
        public int _assignmentcount { get; }

        // Start date of the class
        public string _datebegin { get; }

        // End date of the class
        public string _dateend { get; }

        // Unique ID of the class
        public string _classid { get; }

        // Constructor to initialize class item properties
        public MainWindowBoxClassesItem(string classname, string teachername, int assignmentcount, string datebegin, string dateend, string classid)
        {
            _classname = classname;
            _teachername = teachername;
            _assignmentcount = assignmentcount;
            _datebegin = datebegin;
            _dateend = dateend;
            _classid = classid;
        }
    }
}
