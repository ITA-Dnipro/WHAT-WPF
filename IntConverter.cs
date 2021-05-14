using System;
using System.Windows.Data;
using System.Globalization;

namespace _2048
{
	public class IntConverter : IValueConverter //TODO: Replace
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (int)value;

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
