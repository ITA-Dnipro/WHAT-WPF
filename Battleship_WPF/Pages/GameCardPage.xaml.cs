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
        private Sea _playerMap;
        private Sea _enemyMap;

        public GameCardPage()
        {
            InitializeComponent();

            PlayerCells = new ObservableCollection<CellViewModel>();
            EnemyCells = new ObservableCollection<CellViewModel>();
            AddCells(PlayerCells);
            AddCells(EnemyCells);
            DataContext = this;
        }

        public ObservableCollection<CellViewModel> PlayerCells { get; set; }

        public ObservableCollection<CellViewModel> EnemyCells { get; set; }

        public void AddCells(ObservableCollection<CellViewModel> cells)
        {
            for (int OY = 0; OY < 10; OY++)
            {
                for (int OX = 0; OX < 10; OX++)
                {
                    cells.Add(new CellViewModel(new Position(OY, OX)));
                }
            }

        }
    }
}
