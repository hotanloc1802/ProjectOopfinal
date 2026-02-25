using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using ClassroomManagementApp1.DesignPattern;
using Microsoft.EntityFrameworkCore;
using System.Windows.Media.Imaging;
using System.Windows;
using ClassroomManagementApp1.Factory;

namespace ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxStudentViewModel
{
    public class MainWindowBoxStudentInfoViewModel : ViewModelBase
    {
        private MainWindowBoxStudentInfoItem _item; // Private field for backing store
        public AccountViewModel AccountViewModel { get; private set; }
        private readonly AccountService _accountService;

        // Public property for data binding
        public MainWindowBoxStudentInfoItem Item
        {
            get => _item;
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item)); // Notify that Item has changed
            }
        }

        private BitmapImage _profileImage;

        // Profile image property for data binding
        public BitmapImage ProfileImage
        {
            get => _profileImage;
            private set
            {
                _profileImage = value;
                OnPropertyChanged(nameof(ProfileImage));
            }
        }

        // Constructor with AccountService dependency
        public MainWindowBoxStudentInfoViewModel(AccountService accountService)
        {
            _accountService = accountService;
            AccountViewModel = new AccountViewModel(_accountService);
            InitializeData();
        }

        // Default constructor that uses CreateAccountService
        public MainWindowBoxStudentInfoViewModel() : this(ServiceFactory.CreateAccountService())
        {
        }

        // Method to initialize and load data
        private async void InitializeData()
        {
            try
            {
                // Load account data by student ID
                await AccountViewModel.LoadAccountByStudentIDAsync(StudentContextSingleton.Instance.StudentId);
                await AccountViewModel.GetProfileImageAsync();
                ProfileImage = AccountViewModel.ProfileImage;

                var selectedStudentName = AccountViewModel.SelectedAccountStudent.studentname;

                if (selectedStudentName != null)
                {
                    // Set Item based on account data
                    Item = new MainWindowBoxStudentInfoItem(selectedStudentName, "5");
                }
                else
                {
                    MessageBox.Show("Student account information not found.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                // Handle errors
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
