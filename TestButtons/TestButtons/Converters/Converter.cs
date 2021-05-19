using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TestButtons.Model;

namespace TestButtons.Converters
{
    class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (((CellViewModel)value).MarkTypes != null)
            {
                return ((CellViewModel)value).MarkTypes.Value;
            }
            else
            {
                return ((CellViewModel)value).MarkTypes;
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
