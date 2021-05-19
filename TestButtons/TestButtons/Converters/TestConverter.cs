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
    class TestConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<CellViewModel> cells = new ObservableCollection<CellViewModel>();
            ObservableCollection<CellViewModel> field = (ObservableCollection<CellViewModel>)value;

            foreach (CellViewModel item in field)
            {
                cells.Add(item);
            }
            return cells;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
