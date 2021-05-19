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



namespace Battleship_WPF
{
    /// <summary>
    /// Логика взаимодействия для GameCardPage.xaml
    /// </summary>
    public partial class GameCardPage : Page
    {
        private GameViewModel _currentGame;
        private Level _difficulty;

        public GameCardPage()
        {
            InitializeComponent();
            _difficulty = LevelContainer.Difficulty;
            InitializeGameParameters();
        }

        private void InitializeGameParameters()
        {
            _currentGame = new GameViewModel(_difficulty);
            _currentGame.StartUp();

            PlayerSea.ItemsSource = _currentGame.PlayerCells;
            EnemySea.ItemsSource = _currentGame.EnemyCells;

            DataContext = _currentGame;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages\\DifficultyPage.xaml", UriKind.Relative));
        }
    }
}
