namespace ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxAssignmentsViewModel
{
    public class MainWindowBoxAssignmentsItem : ViewModelBase
    {
        // Description of the assignment
        public string _description { get; }

        // Due date of the assignment
        public string _duedate { get; }

        // Constructor to initialize the assignment item
        public MainWindowBoxAssignmentsItem(string description, string duedate)
        {
            _description = description;
            _duedate = duedate;
        }
    }
}
