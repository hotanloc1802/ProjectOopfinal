using System;
using System.Globalization;
using System.Windows.Data;
namespace ClassroomManagementApp1.ValueConverter
{
    public class MainWindowBoxClassesDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Tuple<DateTime, DateTime> dateRange)
            {
                var startDate = FormatDate(dateRange.Item1);
                var endDate = FormatDate(dateRange.Item2);
                return $"{startDate} - {endDate}";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string FormatDate(DateTime date)
        {
            return $"{date.Day}{GetOrdinalSuffix(date.Day)} {date.ToString("MMM")}";
        }

        private string GetOrdinalSuffix(int day)
        {
            if (day % 100 / 10 == 1) return "th";
            switch (day % 10)
            {
                case 1: return "st";
                case 2: return "nd";
                case 3: return "rd";
                default: return "th";
            }
        }
    }
}

