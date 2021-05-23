﻿using System;
using System.Windows.Data;
using System.Globalization;

namespace _2048.Utils
{
	public class IntConverter : IValueConverter 
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (int)value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
