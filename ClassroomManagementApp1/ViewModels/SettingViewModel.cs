using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.Commands;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using ClassroomManagementApp1.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using ClassroomManagementApp1.ViewModels.ComponentViewModel.MainWindowBoxClassesViewModel;
using ClassroomManagementApp1.Component;
using System.Windows;
namespace ClassroomManagementApp1.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        public AccountViewModel AccountViewModel { get; private set; }

        private readonly AccountService _accounService;
        public class studentinfo : Student
        {
            public string dateofbirth { get; set; }
            public string username { get; set; }
            public studentinfo( string _studentname, string _dateofbirth, string _username)
            {
                studentname = _studentname;
                dateofbirth = _dateofbirth;
                username = _username;
            }
        }
        private studentinfo _studentInfo;
        public studentinfo StudentInfo
        {
            get => _studentInfo;
            set
            {
                _studentInfo = value;
                OnPropertyChanged(nameof(StudentInfo));
            }
        }
        public SettingViewModel(string studentid) : this(CreateDbContext() , studentid)
        {
        }
         public SettingViewModel(AccountService accountService ,string studentid)
        {
            _accounService = accountService;
            AccountViewModel = new AccountViewModel(_accounService);
            InitializeData(studentid);

        }
        private static AccountService CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=OOP-new,public;");
            var context = new AppDbContext(optionsBuilder.Options);
            var accountService = new AccountService(context);
            return (accountService);
        }
        private async void InitializeData(string studentid)
        {
            await AccountViewModel.LoadAccountByStudentIDAsync(studentid);
            StudentInfo = new studentinfo(AccountViewModel.SelectedAccountStudent.studentname,AccountViewModel.SelectedAccountStudent.studentbirth,AccountViewModel.SelectedAccount.username);
        }
    }
}
