﻿using ClassroomManagementApp1.ViewModels.BoxClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClassroomManagementApp1.Component
{
    /// <summary>
    /// Interaction logic for MainWindowBoxAssignments2.xaml
    /// </summary>
    public partial class MainWindowBoxAssignments2 : UserControl    
    {
        public MainWindowBoxAssignments2()
        {
            InitializeComponent();
            DataContext = new MainWindowBoxAssignments2ViewModel();
        }
    }
}