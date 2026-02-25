namespace ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxStudentViewModel
{
    public class MainWindowBoxStudentInfoItem : ViewModelBase
    {
        // Student's name
        public string _studentname { get; }

        // Path or URL to the student's image
        public string _studentimage { get; }

        // Constructor to initialize student information
        public MainWindowBoxStudentInfoItem(string studentname, string studentimage)
        {
            _studentname = studentname;
            _studentimage = studentimage;
        }
    }
}
