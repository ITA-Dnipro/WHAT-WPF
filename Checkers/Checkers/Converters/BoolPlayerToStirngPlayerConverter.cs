using System;
using System.Globalization;
using System.Windows.Data;

namespace Checkers.Converters
{
    public class BoolPlayerToStirngPlayerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return "white checkers";
            }

            return "black checkers";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
