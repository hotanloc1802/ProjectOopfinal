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
using ClassroomManagementApp1.ViewModels.ComponentViewModel;
using ClassroomManagementApp1.ViewModels.ComponentViewModel.AssignmentListViewAssignmentsViewModel;

namespace ClassroomManagementApp1.Component
{
    /// <summary>
    /// Interaction logic for AssigmentListViewAssignments.xaml
    /// </summary>
    public partial class AssigmentListViewAssignments : UserControl
    {
        public AssigmentListViewAssignments()
        {
            InitializeComponent();
            DataContext = new AssignmentListViewAssignmentsViewModel();
            Loaded += OnLoaded; // Run this code once the UI is fully loaded
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Access ScrollViewers of both ListViews
            var firstScrollViewer = GetScrollViewer(FirstListView);
            var secondScrollViewer = GetScrollViewer(SecondListView);

            if (firstScrollViewer != null && secondScrollViewer != null)
            {
                // Attach event handlers to keep both lists in sync
                firstScrollViewer.ScrollChanged += (s, ev) => SyncScroll(firstScrollViewer, secondScrollViewer, ev);
                secondScrollViewer.ScrollChanged += (s, ev) => SyncScroll(secondScrollViewer, firstScrollViewer, ev);
            }
        }

        private void SyncScroll(ScrollViewer source, ScrollViewer target, ScrollChangedEventArgs e)
        {
            // Update vertical offset of the target to match the source
            if (e.VerticalChange != 0)
            {
                target.ScrollToVerticalOffset(source.VerticalOffset);
            }
        }

        private ScrollViewer GetScrollViewer(DependencyObject obj)
        {
            if (obj is ScrollViewer) return (ScrollViewer)obj;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                var child = VisualTreeHelper.GetChild(obj, i);
                var result = GetScrollViewer(child);
                if (result != null) return result;
            }
            return null;
        }
    }
}
