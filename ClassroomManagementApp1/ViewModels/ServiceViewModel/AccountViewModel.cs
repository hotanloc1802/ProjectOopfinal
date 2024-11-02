using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomManagementApp1.ViewModels.ServiceViewModels
{
    public class AccountViewModel
    {
        private readonly AccountService _accountService;

        // ObservableCollection to notify UI of changes
        private Account _selectedAccount;
        public Account SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
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
        public AccountViewModel(AccountService AccountService)
        {
            _accountService= AccountService;
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

    }
}
