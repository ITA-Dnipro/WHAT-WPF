using System.Windows;

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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TetrisStartView tetrisStart = new TetrisStartView();

            this.Close();
            tetrisStart.Show();
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
