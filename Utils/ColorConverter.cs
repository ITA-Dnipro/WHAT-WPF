using System;
using System.Windows.Data;
using System.Globalization;
using _2048.Enums;

namespace _2048.Utils
{
	public class ColorConverter : IValueConverter 
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string color = Enum.GetName(typeof(CellColor), (int)value);
			return color;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
