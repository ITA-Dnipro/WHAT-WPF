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

using BattleshipLibrary;

namespace Battleship_WPF
{
    /// <summary>
    /// Логика взаимодействия для GameCardPage.xaml
    /// </summary>
    public partial class GameCardPage : Page
    {
        public GameViewModel currentGame;

        public GameCardPage()
        {
            InitializeComponent();

            currentGame = new GameViewModel(Level.Meduim);
            currentGame.StartUp();

            PlayerCells = currentGame.PlayerCells;
            EnemyCells = currentGame.EnemyCells;

            DataContext = this;
        }

        public ObservableCollection<CellViewModel> PlayerCells { get; set; }

        public ObservableCollection<CellViewModel> EnemyCells { get; set; }

        private void CellButton_Click(object sender, RoutedEventArgs e)
        {
            Button cell = (Button)sender;
            Position coord = (Position)cell.DataContext;
            currentGame.GetPlayerShot(coord);
        }
    }
}
