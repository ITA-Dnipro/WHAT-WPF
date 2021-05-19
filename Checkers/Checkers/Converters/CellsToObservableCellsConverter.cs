using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

using Checkers.Models.Interfaces;

namespace Checkers.Converters
{
    public class CellsToObservableCellsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<ICell> cells = new ObservableCollection<ICell>();

            foreach (var cell in (ICell[,])value)
            {
                cells.Add(cell);
            }

            return cells;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
