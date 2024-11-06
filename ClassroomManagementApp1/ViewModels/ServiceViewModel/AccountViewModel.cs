using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Models;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ClassroomManagementApp1.ViewModels.ServiceViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private readonly AccountService _accountService;
        private Account _selectedAccount;
        
        private BitmapImage _profileImage;

        public BitmapImage ProfileImage
        {
            get => _profileImage;
            private set
            {
                _profileImage = value;
                OnPropertyChanged(nameof(ProfileImage));
            }
        }
        
        public Account SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                OnPropertyChanged(nameof(SelectedAccount));
                LoadProfileImage(); // Load image when the account is selected
            }
        }
        private Student _selectedAccountStudent;
        public Student SelectedAccountStudent
        {
            get => _selectedAccountStudent;
            set
            {
                _selectedAccountStudent = value;
            }
        }
        public AccountViewModel(AccountService accountService)
        {
            _accountService = accountService;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadAccountByStudentIDAsync(string studentId)
        {
            var account = await _accountService.GetAccountByStudentID(studentId);
            if (account != null)
            {
                SelectedAccount = account; // Assign fetched account to SelectedAccount
                SelectedAccountStudent = account.Student;
            }
        }

        public async void LoadProfileImage()
        {
            if (SelectedAccount != null)
            {
                ProfileImage = await GetProfileImageAsync();
            }
        }

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
                    image.Freeze();
                    return image;
                }
            }
            return null; // Hoặc hình ảnh mặc định
        }
       
    }
}
