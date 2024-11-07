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
using ClassroomManagementApp1.ViewModels;
using Microsoft.Win32;
namespace ClassroomManagementApp1.Component
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ClassesListViewClasses : UserControl
    {
        
        public string ClassId
        {
            get { return (string)GetValue(ClassIdProperty); }
            set { SetValue(ClassIdProperty, value); }
        }

        public static readonly DependencyProperty ClassIdProperty =
            DependencyProperty.Register("ClassId", typeof(string), typeof(ClassesListViewClasses), new PropertyMetadata(string.Empty, OnClassIdChanged));

        public ClassesListViewClasses()
        {
            InitializeComponent();
        }

        private static void OnClassIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ClassesListViewClasses;
            if (control != null)
            {
                // Cập nhật DataContext hoặc thực hiện các hành động cần thiết khi ClassId thay đổi
                control.DataContext = new ClassesViewModel(e.NewValue.ToString());
            }
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Tìm Button từ sender
            Button button = sender as Button;

            // Kiểm tra xem button có hợp lệ không
            if (button != null)
            {
                // Lấy item DataContext từ Button
                var listViewItem = FindParent<ListViewItem>(button);
                if (listViewItem != null)
                {
                    var assignment = listViewItem.DataContext; // Đây là item hiện tại

                    // Tìm TextBox trong item DataTemplate
                    TextBox filePathTextBox = FindVisualChild<TextBox>(listViewItem);
                    if (filePathTextBox != null)
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "All Files (*.*)|*.*|PDF Files (*.pdf)|*.pdf|Word Documents (*.docx)|*.docx";

                        // Hiển thị hộp thoại cho người dùng và kiểm tra xem họ đã chọn tệp hay chưa
                        if (openFileDialog.ShowDialog() == true)
                        {
                            // Lấy đường dẫn tệp được chọn
                            string selectedFilePath = openFileDialog.FileName;

                            // Hiển thị đường dẫn vào TextBox
                            filePathTextBox.Text = selectedFilePath;

                            // Thông báo cho người dùng về tệp đã chọn (tùy chọn)
                            MessageBox.Show("Chosen file: " + selectedFilePath);
                        }
                    }
                }
            }
        }

        // Tìm phần tử cha trong Visual Tree
        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            T parent = parentObject as T;
            return parent != null ? parent : FindParent<T>(parentObject);
        }

        // Tìm phần tử con trong Visual Tree
        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T visualChild)
                {
                    return visualChild;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }



    }

}
