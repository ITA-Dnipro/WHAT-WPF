using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using TestButtons.Model;

namespace TestButtons.Control
{
    /// <summary>
    /// Логика взаимодействия для TicTocControl.xaml
    /// </summary>
    public partial class TicTocControl : UserControl
    {
        public static DependencyProperty _quantity = DependencyProperty.Register(nameof(Quantity), typeof(int), typeof(TicTocControl));
        public static DependencyProperty _field = DependencyProperty.Register(nameof(MyField), typeof(ObservableCollection<CellViewModel>), typeof(TicTocControl));

        public TicTocControl()
        {
            InitializeComponent();
        }

        public int Quantity
        {
            get
            {
                return (int)GetValue(_quantity);
            }
            set
            {
                SetValue(_quantity, value);
            }
        }


        public ObservableCollection<CellViewModel> MyField
        {
            get
            {
                return (ObservableCollection<CellViewModel>)GetValue(_field);
            }
            set
            {
                SetValue(_field, value);
            }
        }

    }
}
