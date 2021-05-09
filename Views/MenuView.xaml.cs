using System;
using System.Collections.Generic;
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

namespace Tetris.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : Window
    {
        public MenuView()
        {
            InitializeComponent();
        }

       // private Window _window;

        //private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    this.Width = _window.Width;
        //    this.Height = _window.Height;
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TetrisStartView tetrisStart = new TetrisStartView();

            this.Close();
            tetrisStart.Show();
           // NavigationService.Navigate(new Uri("Views/GameStartView.xaml", UriKind.Relative));
        }

        //private void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //     _window = Window.GetWindow(this) as MainWindow;
        //    _window.Background = this.Background;

        //    _window.SizeChanged += Window_SizeChanged;
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
