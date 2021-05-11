using BattleshipLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_WPF
{
    public class GameViewModel
    {
        #region Consts

        const int HEIGTH = 10;
        const int WIDTH = 10;

        #endregion

        #region Private

        private Sea _playerMap;
        private Sea _enemyMap;
        private bool _isPlayerWinner;
        private bool _isAlivePlayerAfterRigthShoot;
        private bool _isEasyLevel;
        private Intelligence _enemysMind;
        private bool _isTargetPlayer;
        private bool _isTargetEnemy;
        private Level _currentLevel;

        #endregion

        public GameViewModel(Level currentLevel)
        {
            _playerMap = null;
            _enemyMap = null;
            _isPlayerWinner = false;
            _isAlivePlayerAfterRigthShoot = false;
            _enemysMind = null;
            _isTargetEnemy = false;
            _isTargetPlayer = false;
            _currentLevel = currentLevel;
        }

        public ObservableCollection<CellViewModel> PlayerCells { get; set; }

        public ObservableCollection<CellViewModel> EnemyCells { get; set; }

        public void StartUp()
        {
            _playerMap = new Sea(10);
            _enemyMap = new Sea(10);
            _playerMap.BuildAllTypeOfShips();
            _enemyMap.BuildAllTypeOfShips();
            PlayerCells = new ObservableCollection<CellViewModel>();
            EnemyCells = new ObservableCollection<CellViewModel>();
            AddCells(PlayerCells, _playerMap, false);
            AddCells(EnemyCells, _enemyMap, true);
            SetLevel();
        }

        public void AddCells(ObservableCollection<CellViewModel> cells, Sea map, bool isEnemyMap)
        {
            for (int Y = 0; Y < HEIGTH; Y++)
            {
                for (int X = 0; X < WIDTH; X++)
                {
                    MapCondition condition = map[Y, X];
                    string imagePath = GetImagePath(condition, isEnemyMap);
                    cells.Add(new CellViewModel(new Position(Y, X), imagePath));
                }
            }
        }

        public string GetImagePath(MapCondition condition, bool isEnemyMap)
        {
            if (isEnemyMap)
            {
                return "Resourses/WaveCell.png";
            }

            string path = string.Empty;

            switch (condition)
            {
                case MapCondition.MissedShot:
                    path = "Resourses/PastCell.png";
                    break;
                case MapCondition.NoneShot:
                    path = "Resourses/WaveCell.png";
                    break;
                case MapCondition.ShipSafe:
                    path = "Resourses/ShipCell.png";
                    break;
                case MapCondition.ShipInjured:
                    path = "Resourses/BombCell.png";
                    break;
                case MapCondition.ShipDestroyed:
                    path = "Resourses/DestructionCell.png";
                    break;
                default:
                    break;
            }

            return path;
        }

        public void SetLevel()
        {
            switch (_currentLevel)
            {
                case Level.Easy:
                    _isEasyLevel = true;
                    break;
                case Level.Meduim:
                    _enemysMind = new Intelligence(-1, -1);
                    break;
                case Level.Hard:
                    _enemysMind = new AdvancedIntelligence(-1, -1);
                    break;
                default:
                    break;
            }
        }

        public void GetPlayerShot(Position coord)
        {

        }

    }
}
