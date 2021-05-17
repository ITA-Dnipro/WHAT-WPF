using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Checkers.Models.Interfaces;

namespace Checkers.Controls
{
    public partial class CheckerBoard : UserControl
    {
        private static DependencyProperty _rowCount = 
            DependencyProperty.Register(nameof(RowCount), typeof(int), typeof(CheckerBoard));

        private static DependencyProperty _columnCount = 
            DependencyProperty.Register(nameof(ColumnCount), typeof(int), typeof(CheckerBoard)); 

        private static DependencyProperty _cellsProperty = 
            DependencyProperty.Register(nameof(Cells), typeof(ObservableCollection<ICell>), typeof(CheckerBoard));

        public CheckerBoard()
        {
            InitializeComponent();
        }

        public int RowCount 
        {
            get => (int)GetValue(_rowCount);
            set => SetValue(_rowCount, value);
        }

        public int ColumnCount 
        {
            get => (int)GetValue(_columnCount);
            set => SetValue(_columnCount, value);
        }

        public ObservableCollection<ICell> Cells 
        {
            get => (ObservableCollection<ICell>)GetValue(_cellsProperty);
            set => SetValue(_cellsProperty, value);
        }
    }
}
