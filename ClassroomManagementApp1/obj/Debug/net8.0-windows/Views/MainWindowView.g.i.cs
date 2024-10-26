﻿#pragma checksum "..\..\..\..\Views\MainWindowView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AF76B23CF3E5FB23C3F9C552612FAED0F514C302"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ClassroomManagementApp1.Component;
using ClassroomManagementApp1.Views;
using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ClassroomManagementApp1.Views {
    
    
    /// <summary>
    /// MainWindowView
    /// </summary>
    public partial class MainWindowView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\..\Views\MainWindowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDashboard;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\Views\MainWindowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClassroom;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Views\MainWindowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAssignment;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\Views\MainWindowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSetting;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\Views\MainWindowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSignOut;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\..\Views\MainWindowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border boSearch;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\..\Views\MainWindowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchTextBox;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\..\..\Views\MainWindowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnViewAll;
        
        #line default
        #line hidden
        
        
        #line 204 "..\..\..\..\Views\MainWindowView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border boStudentInfo;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ClassroomManagementApp1;component/views/mainwindowview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\MainWindowView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btnDashboard = ((System.Windows.Controls.Button)(target));
            return;
            case 2:
            this.btnClassroom = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\..\Views\MainWindowView.xaml"
            this.btnClassroom.Click += new System.Windows.RoutedEventHandler(this.BtnClassroom_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnAssignment = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\..\Views\MainWindowView.xaml"
            this.btnAssignment.Click += new System.Windows.RoutedEventHandler(this.BtnAssignment_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnSetting = ((System.Windows.Controls.Button)(target));
            
            #line 65 "..\..\..\..\Views\MainWindowView.xaml"
            this.btnSetting.Click += new System.Windows.RoutedEventHandler(this.Setting_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnSignOut = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.boSearch = ((System.Windows.Controls.Border)(target));
            return;
            case 7:
            this.SearchTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.btnViewAll = ((System.Windows.Controls.Button)(target));
            
            #line 143 "..\..\..\..\Views\MainWindowView.xaml"
            this.btnViewAll.Click += new System.Windows.RoutedEventHandler(this.BtnClassroom_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.boStudentInfo = ((System.Windows.Controls.Border)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

