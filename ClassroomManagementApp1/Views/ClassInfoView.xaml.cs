using ClassroomManagementApp1.Component;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassroomManagementApp1.Component;
namespace ClassroomManagementApp1.Views
{
    /// <summary>
    /// Interaction logic for ClassInfo.xaml
    /// </summary>
    public partial class ClassInfoView : Window
    {
        public event EventHandler RequestClose;
        public ClassInfoView()
        {
            InitializeComponent();
            var classInfoBoxAssignments = new ClassInfoBoxAssignments(); // Tạo UserControl mà không cần truyền DataContext                                                 // Thêm UserControl vào giao diện
            MainGrid.Children.Add(classInfoBoxAssignments);
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
        private void OK_Click(object obj, RoutedEventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Kiểm tra kết quả chọn của người dùng
            if (result == MessageBoxResult.Yes)
            {
                this.Close(); // Đóng cửa sổ nếu chọn Yes
            }
            // Nếu chọn No, không làm gì cả
        }
    }
}
