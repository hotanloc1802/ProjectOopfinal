using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;

namespace ClassroomManagementApp1.ViewModels.BoxClasses
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

        // Constructor with AccountService dependency
        public MainWindowBoxStudentInfoViewModel(AccountService accountService)
        {
            _accountService = accountService;
            AccountViewModel = new AccountViewModel(_accountService);
            InitializeData();
        }

        // Method to create and return AccountService instance
        private static AccountService CreateAccountService()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=OOP-new,public;");
            var context = new AppDbContext(optionsBuilder.Options);
            return new AccountService(context);
        }

        // Default constructor that uses CreateAccountService
        public MainWindowBoxStudentInfoViewModel() : this(CreateAccountService())
        {
        }

        // Method to initialize and load data
        private async void InitializeData()
        {
            try
            {
                // Load account data by student ID
                await AccountViewModel.LoadAccountByStudentIDAsync("S001");
                var _accountSelected = AccountViewModel.SelectedAccount;

                if (_accountSelected != null)
                {
                    // Set Item based on account data
                    Item = new MainWindowBoxStudentInfoItem(_accountSelected.username, _accountSelected.userid);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin tài khoản của sinh viên.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                // Handle errors
                MessageBox.Show($"Có lỗi xảy ra khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
