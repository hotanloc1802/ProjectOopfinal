using System;
using System.Globalization;
using System.Windows.Data;

namespace ClassroomManagementApp1.ValueConverter
{
    public class PasswordToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value as string; // Convert password to string
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value as string; // Convert string back to password
        }
    }
}
