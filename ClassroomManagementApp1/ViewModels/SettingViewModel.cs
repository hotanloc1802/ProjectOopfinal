using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassroomManagement.Application.Services;
using ClassroomManagement.Domain.Entities;
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

        private readonly IAccountService _accounService;
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
        public SettingViewModel(IAccountService accountService, string studentid)
        {
            _accounService = accountService;
            AccountViewModel = new AccountViewModel(_accounService);
            InitializeData(studentid);

        }
        
        private async void InitializeData(string studentid)
        {
            await AccountViewModel.LoadAccountByStudentIDAsync(studentid);
            StudentInfo = new studentinfo(AccountViewModel.SelectedAccountStudent.studentname,AccountViewModel.SelectedAccountStudent.studentbirth,AccountViewModel.SelectedAccount.username);
        }
    }
}
