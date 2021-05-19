using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestButtons.Control;
using TestButtons.ViewModel;

namespace TestButtons
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private int Quantity { get; set; }
       

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Quantity = int.Parse((string)comboBox1.SelectedItem);
            ((MainWindowViewModel)DataContext).Quantity = Quantity;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}