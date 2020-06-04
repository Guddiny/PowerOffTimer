using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace PowerOffTimer.Converters
{
    public class TimeToNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            ((DateTime)value).Ticks;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new DateTime(System.Convert.ToInt64(value));
        }
    }
}
