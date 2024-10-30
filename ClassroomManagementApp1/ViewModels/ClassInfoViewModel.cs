using System;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using ClassroomManagementApp1.ViewModels.BoxClasses;
using ClassroomManagementApp1.ClassService;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.Models;
using ClassroomManagementApp1.ViewModels.ServiceViewModels;
using ClassroomManagementApp1.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace ClassroomManagementApp1.ViewModels
{
    public class ClassInfoViewModel : ViewModelBase
    {
        public ClassViewModel ClassViewModel { get; private set; }
        public AssignmentViewModel AssignmentViewModel { get; private set; }
        private readonly ClassesService _classService;
        private readonly AssignmentService _assignmentService;
        private Class _classtInfo;
        public Class ClassInfo
        {
            get => _classtInfo;
            set
            {
                _classtInfo = value;
                OnPropertyChanged(nameof(ClassInfo));
            }
        }
        
        public ClassInfoViewModel(ClassesService classService, AssignmentService assignmentService,string classID)
        {
            _classService = classService;
            _assignmentService = assignmentService;
            ClassViewModel = new ClassViewModel(_classService);
            AssignmentViewModel = new AssignmentViewModel(_assignmentService);
            var _classid = classID;
            InitializeData(_classid);
        }
        private static (ClassesService, AssignmentService) CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=OOP-new,public;");
            var context = new AppDbContext(optionsBuilder.Options);
            var classesService = new ClassesService(context);
            var assignmentService = new AssignmentService(context);
            return (classesService, assignmentService);

        }
        public ClassInfoViewModel(string classID) : this(CreateDbContext().Item1, CreateDbContext().Item2, classID)
        {
        }
        private async void InitializeData( string _classid)
        {
            await ClassViewModel.LoadClassByIdAsync(_classid);
            ClassInfo = ClassViewModel.SelectedClass;
        }
    }
}