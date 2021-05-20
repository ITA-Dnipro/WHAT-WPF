
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Battleship_WPF
{
    public class GameViewModel : BaseViewModel
    {
        #region Consts

        const int HEIGTH = 10;
        const int WIDTH = 10;
        const string WIN = "YOU WIN!";
        const string LOSE = "YOU LOSE!";

        #endregion

        #region Private

        private Sea _playerMap;
        private Sea _enemyMap;
        private bool _isAlivePlayerAfterRigthShoot;
        private bool _isEasyLevel;
        private Intelligence _enemysMind;
        private bool _isTargetPlayer;
        private bool _isTargetEnemy;
        private Level _currentLevel;
        private CellCommand _clickButton;
        private string _targetCoords;
        private PositionFormatter _formatter;
        private int _fourDeckEnemyShipsCount;
        private int _threeDeckEnemyShispCount;
        private int _twoDeckEnemyShipsCount;
        private int _oneDeckEnemyShipsCount;
        private int _fourDeckPlayerShipsCount;
        private int _threeDeckPlayerShispCount;
        private int _twoDeckPlayerShipsCount;
        private int _oneDeckPlayerShipsCount;
        private string _winnerSatus;
        private bool _isVisible;
        private bool _isPlayerTurn;
        #endregion

        public GameViewModel(Level currentLevel)
        {
            _currentLevel = currentLevel;
        }

        public ObservableCollection<CellViewModel> PlayerCells { get; set; }

        public ObservableCollection<CellViewModel> EnemyCells { get; set; }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        public string WinnerStatus
        {
            get => _winnerSatus;
            set
            {
                _winnerSatus = value;
                OnPropertyChanged("WinnerStatus");
            }
        }

        public string TargetCoords
        {
            get => _targetCoords;
            set
            {
                _targetCoords = value;
                OnPropertyChanged("TargetCoords");
            }
        }

        public CellCommand ClickButton
        {
            get
            {
                return _clickButton ??
                    (_clickButton = new CellCommand(obj =>
                    {
                        CellViewModel cell = obj as CellViewModel;

                        GetPlayerShot(cell.Coord);
                    },
                    obj => obj is CellViewModel cell && _isPlayerTurn));
            }
        }

        public void StartUp()
        {
            _isAlivePlayerAfterRigthShoot = false;
            _isTargetEnemy = false;
            _isTargetPlayer = false;
            _isPlayerTurn = true;
            _targetCoords = string.Empty;
            _playerMap = new Sea(HEIGTH);
            _enemyMap = new Sea(HEIGTH);
            _formatter = new PositionFormatter();
            _playerMap.BuildAllTypeOfShips();
            _enemyMap.BuildAllTypeOfShips();
            PlayerCells = new ObservableCollection<CellViewModel>();
            EnemyCells = new ObservableCollection<CellViewModel>();
            AddCells(PlayerCells, _playerMap, false);
            AddCells(EnemyCells, _enemyMap, true);
            SetLevel();

            _playerMap.InjuredShip += ChangeInjuredCellOfPlayer;
            _playerMap.DestroyedShip += ChangeDestroyedCellOfPlayer;
            _playerMap.MissedCell += ChangePastCellOfPlayer;

            _enemyMap.InjuredShip += ChangeInjuredCellOfEnemy;
            _enemyMap.DestroyedShip += ChangeDestroyedCellOfEnemy;
            _enemyMap.MissedCell += ChangePastCellOfEnemy;

            FourDeckEnemyShipsCount = _enemyMap.FourDeckShipCount;
            ThreeDeckEnemyShipsCount = _enemyMap.ThreeDeckShipCount;
            TwoDeckEnemyShipsCount = _enemyMap.TwoDeckShipCount;
            OneDeckEnemyShipsCount = _enemyMap.OneDeckShipCount;
            FourDeckPlayerShipsCount = _playerMap.FourDeckShipCount;
            ThreeDeckPlayerShipsCount = _playerMap.ThreeDeckShipCount;
            TwoDeckPlayerShipsCount = _playerMap.TwoDeckShipCount;
            OneDeckPlayerShipsCount = _playerMap.OneDeckShipCount;
        }

        private void AddCells(ObservableCollection<CellViewModel> cells, Sea map, bool isEnemyMap)
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

        private string GetImagePath(MapCondition condition, bool isEnemyMap)
        {
            if (isEnemyMap)
            {
                return "WaveCell";
            }

            string path = string.Empty;

            switch (condition)
            {
                case MapCondition.MissedShot:
                    path = "PastCell";
                    break;
                case MapCondition.NoneShot:
                    path = "WaveCell";
                    break;
                case MapCondition.ShipSafe:
                    path = "ShipCell";
                    break;
                case MapCondition.ShipInjured:
                    path = "BombCell";
                    break;
                case MapCondition.ShipDestroyed:
                    path = "DestructionCell";
                    break;
                default:
                    break;
            }

            return path;
        }

        private void SetLevel()
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

        public void GetPlayerShot(Position coords)
        {
            bool shipSearched = _enemyMap.SearchShips();

            if (!shipSearched)
            {
                return;
            }

            _enemyMap.TargetCoordY = coords.OY;
            _enemyMap.TargetCoordX = coords.OX;

            bool wasShot = _enemyMap.WasShot();

            if (wasShot)
            {
                return;
            }

            bool isFinishedOfShipEnemy = false;
            _isTargetEnemy = _enemyMap.HitTarget(ref isFinishedOfShipEnemy);

            if (_isTargetEnemy && !isFinishedOfShipEnemy)
            {
                _enemyMap.MarkImpossibleTargets();
            }

            FourDeckEnemyShipsCount = _enemyMap.FourDeckShipCount;
            ThreeDeckEnemyShipsCount = _enemyMap.ThreeDeckShipCount;
            TwoDeckEnemyShipsCount = _enemyMap.TwoDeckShipCount;
            OneDeckEnemyShipsCount = _enemyMap.OneDeckShipCount;

            TargetCoords = _formatter.GetPosition(new Position(_enemyMap.TargetCoordY,
                _enemyMap.TargetCoordX));

            if (!_enemyMap.SearchShips())
            {
                WinnerStatus = WIN;
                IsVisible = true;
                return;
            }

            if (!_isTargetEnemy)
            {
                _isPlayerTurn = false;
                GetEnemyShot();
            }
        }

        public int FourDeckEnemyShipsCount
        {
            get => _fourDeckEnemyShipsCount;
            set
            {
                _fourDeckEnemyShipsCount = value;
                OnPropertyChanged("FourDeckEnemyShipsCount");
            }
        }

        public int ThreeDeckEnemyShipsCount
        {
            get => _threeDeckEnemyShispCount;
            set
            {
                _threeDeckEnemyShispCount = value;
                OnPropertyChanged("ThreeDeckEnemyShipsCount");
            }
        }

        public int TwoDeckEnemyShipsCount
        {
            get => _twoDeckEnemyShipsCount;
            set
            {
                _twoDeckEnemyShipsCount = value;
                OnPropertyChanged("TwoDeckEnemyShipsCount");
            }
        }

        public int OneDeckEnemyShipsCount
        {
            get => _oneDeckEnemyShipsCount;
            set
            {
                _oneDeckEnemyShipsCount = value;
                OnPropertyChanged("OneDeckEnemyShipsCount");
            }
        }

        public int FourDeckPlayerShipsCount
        {
            get => _fourDeckPlayerShipsCount;
            set
            {
                _fourDeckPlayerShipsCount = value;
                OnPropertyChanged("FourDeckPlayerShipsCount");
            }
        }

        public int ThreeDeckPlayerShipsCount
        {
            get => _threeDeckPlayerShispCount;
            set
            {
                _threeDeckPlayerShispCount = value;
                OnPropertyChanged("ThreeDeckPlayerShipsCount");
            }
        }

        public int TwoDeckPlayerShipsCount
        {
            get => _twoDeckPlayerShipsCount;
            set
            {
                _twoDeckPlayerShipsCount = value;
                OnPropertyChanged("TwoDeckPlayerShipsCount");
            }
        }

        public int OneDeckPlayerShipsCount
        {
            get => _oneDeckPlayerShipsCount;
            set
            {
                _oneDeckPlayerShipsCount = value;
                OnPropertyChanged("OneDeckPlayerShipsCount");
            }
        }

        private async void GetEnemyShot()
        {
            if (_isEasyLevel)
            {
                await Task.Run(() => ShotWithoutIntellegence());
            }
            else
            {
                await Task.Run(() => ShotWithEnemyIntellegence());
            }

        }

        private void ShotWithEnemyIntellegence()
        {
            do
            {
                System.Threading.Thread.Sleep(1000);
                _enemysMind.MakeTheShot(ref _isAlivePlayerAfterRigthShoot, _playerMap);
                _isTargetPlayer = _enemysMind.IsTargetPlayer;
                _playerMap.CheckShipCondition(_isTargetPlayer, _isAlivePlayerAfterRigthShoot,
                        _enemysMind);

                TargetCoords = _formatter.GetPosition(new Position(_playerMap.TargetCoordY,
                        _playerMap.TargetCoordX));

                FourDeckPlayerShipsCount = _playerMap.FourDeckShipCount;
                ThreeDeckPlayerShipsCount = _playerMap.ThreeDeckShipCount;
                TwoDeckPlayerShipsCount = _playerMap.TwoDeckShipCount;
                OneDeckPlayerShipsCount = _playerMap.OneDeckShipCount;

                if (!_playerMap.SearchShips())
                {
                    WinnerStatus = LOSE;
                    IsVisible = true;
                    break;
                }

            } while (_isTargetPlayer);

            _isPlayerTurn = true;
        }

        private void ShotWithoutIntellegence()
        {
            do
            {
                System.Threading.Thread.Sleep(2000);
                RandomCoords.SearchRandomCoords(_playerMap);
                _isTargetPlayer = _playerMap.HitTarget(ref _isAlivePlayerAfterRigthShoot);

                TargetCoords = _formatter.GetPosition(new Position(_playerMap.TargetCoordY,
                        _playerMap.TargetCoordX));

                FourDeckPlayerShipsCount = _playerMap.FourDeckShipCount;
                ThreeDeckPlayerShipsCount = _playerMap.ThreeDeckShipCount;
                TwoDeckPlayerShipsCount = _playerMap.TwoDeckShipCount;
                OneDeckPlayerShipsCount = _playerMap.OneDeckShipCount;

                bool shipSearched = _playerMap.SearchShips();

                if (!shipSearched)
                {
                    WinnerStatus = LOSE;
                    IsVisible = true;
                    break;
                }

            } while (_isTargetPlayer);

            _isPlayerTurn = true;
        }

        private void ChangeInjuredCellOfPlayer(object sender, InjuredShipEventArgs e)
        {
            foreach (var cell in PlayerCells)
            {
                if (cell.Coord.OX == e.InjuredCell.OX && cell.Coord.OY == e.InjuredCell.OY)
                {
                    cell.StyleKey = "BombCell";
                    break;
                }
            }
        }

        private void ChangeDestroyedCellOfPlayer(object sender, DestroyedShipEventArgs e)
        {
            for (int i = 0; i < e.DestroyedShip.CountOfDeck; i++)
            {
                foreach (var cell in PlayerCells)
                {
                    if (cell.Coord.OX == e.DestroyedShip[i].OX && cell.Coord.OY == e.DestroyedShip[i].OY)
                    {
                        cell.StyleKey = "DestructionCell";
                        break;
                    }
                }
            }
        }

        private void ChangePastCellOfPlayer(object sender, MissedShotEventArgs e)
        {
            foreach (var cell in PlayerCells)
            {
                if (cell.Coord.OX == e.MissedPosition.OX && cell.Coord.OY == e.MissedPosition.OY)
                {
                    cell.StyleKey = "PastCell"; ;
                    break;
                }
            }
        }

        private void ChangeInjuredCellOfEnemy(object sender, InjuredShipEventArgs e)
        {
            foreach (var cell in EnemyCells)
            {
                if (cell.Coord.OX == e.InjuredCell.OX && cell.Coord.OY == e.InjuredCell.OY)
                {
                    cell.StyleKey = "BombCell";
                    break;
                }
            }
        }

        private void ChangeDestroyedCellOfEnemy(object sender, DestroyedShipEventArgs e)
        {
            for (int i = 0; i < e.DestroyedShip.CountOfDeck; i++)
            {
                foreach (var cell in EnemyCells)
                {
                    if (cell.Coord.OX == e.DestroyedShip[i].OX && cell.Coord.OY == e.DestroyedShip[i].OY)
                    {
                        cell.StyleKey = "DestructionCell";
                        break;
                    }
                }
            }
        }

        private void ChangePastCellOfEnemy(object sender, MissedShotEventArgs e)
        {
            foreach (var cell in EnemyCells)
            {
                if (cell.Coord.OX == e.MissedPosition.OX && cell.Coord.OY == e.MissedPosition.OY)
                {
                    cell.StyleKey = "PastCell";
                    break;
                }
            }
        }
    }
}
