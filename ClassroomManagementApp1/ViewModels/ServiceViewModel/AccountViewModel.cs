using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;

namespace ClassroomManagementApp1.ViewModels.ServiceViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private readonly IAccountService _accountService;
        private Account _selectedAccount;

        private BitmapImage _profileImage;

        // Property for binding the profile image
        public BitmapImage ProfileImage
        {
            get => _profileImage;
            private set
            {
                _profileImage = value;
                OnPropertyChanged(nameof(ProfileImage));
            }
        }

        // Property for binding the selected account
        public Account SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                OnPropertyChanged(nameof(SelectedAccount));
                LoadProfileImage(); // Load profile image when an account is selected
            }
        }

        private Student _selectedAccountStudent;

        // Property for binding the student related to the selected account
        public Student SelectedAccountStudent
        {
            get => _selectedAccountStudent;
            set
            {
                _selectedAccountStudent = value;
            }
        }

        // Constructor accepting an AccountService dependency
        public AccountViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // Event for property change notification
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Load account information by student ID
        public async Task LoadAccountByStudentIDAsync(string studentId)
        {
            var account = await _accountService.GetAccountByStudentID(studentId);
            if (account != null)
            {
                SelectedAccount = account; // Assign fetched account to SelectedAccount
                SelectedAccountStudent = account.Student;
            }
        }

        // Load the profile image of the selected account
        public async void LoadProfileImage()
        {
            if (SelectedAccount != null)
            {
                ProfileImage = await GetProfileImageAsync();
            }
        }

        // Get the profile image asynchronously
        public async Task<BitmapImage> GetProfileImageAsync()
        {
            if (SelectedAccount?.profilepicture != null)
            {
                using (var ms = new MemoryStream(SelectedAccount.profilepicture))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = ms;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    image.Freeze(); // Freeze to make it thread-safe
                    return image;
                }
            }
            return null; // Or return a default image
        }
    }
}
