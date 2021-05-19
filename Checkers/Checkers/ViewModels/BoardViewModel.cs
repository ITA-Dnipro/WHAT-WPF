using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Input;

using Checkers.Models.Commands;
using Checkers.Models.Interfaces;
using Checkers.Models;

namespace Checkers.ViewModels
{
    public class BoardViewModel : Notifier
    {
        private bool _isFirstPlayer = true;
        private bool _isHappened = false;

        private int _countWhiteCheckers = 12;
        private int _countBlackCheckers = 12;
        private string _winnerMessage;

        private Color _colorOfSelectedCell;
        private Color _colorOfUnselectedCell;
        private Color _colorOfCellsToDoTurn;
        
        private ICommand _pressing;
        private ICommand _setNewGame;
        private ICommand _exit;

        private ICell _mainCell;
        private ICell _dependentCell;
        private ICell _mainMustAttack;

        private List<ICell> _mustAtteck;
        private List<ICell> _paintedCells;

        public ICell[,] Board { get; private set; }

        public int Size => 8;

        public int CountWhiteCheckers => _countWhiteCheckers;
        public int CountBlackCheckers => _countBlackCheckers;
        public bool IsFirstPlayer => _isFirstPlayer;

        public int ZIndexToWinner => HaveWinner() ? 0 : 2;

        public string WinnerMessage 
        {
            get => _winnerMessage;
            set 
            {
                _winnerMessage = value;

                OnPropertyChanged();
            }
        }

        public BoardViewModel()
        {
            SetColor();

            SetBoard();
        }

        public ICommand Pressing => _pressing ?? (_pressing = new RelayCommand(pressedCell =>
        {
            ICell cell = pressedCell as ICell;

            if (SetMainCell(cell))
            {
                return;
            }

            SetDependencyCell(cell);
        }));

        public ICommand SetNewGame => _setNewGame ?? (_setNewGame = new RelayCommand(parameter =>
        {
            Reset();
        }));

        public ICommand Exit => _exit ?? (_exit = new RelayCommand(parameter =>
        {
            Environment.Exit(0);
        }));

        private void OnBoardChanged()
        {
            OnPropertyChanged(nameof(Board));
            OnPropertyChanged(nameof(ZIndexToWinner));
        }

        private void SetBoard() 
        {
            Board = new ICell[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if ((((i % 2 == 0) && (j % 2 == 0)) || ((i % 2 == 1) && (j % 2 == 1))) && i < 3)
                    {
                        Board[i, j] = new CheckerCell
                        {
                            Row = i,
                            Column = j,
                            Checker = new WhiteChecker(i, j),
                            Background = _colorOfUnselectedCell
                        };

                        continue;
                    }

                    if ((((i % 2 == 0) && (j % 2 == 0)) || ((i % 2 == 1) && (j % 2 == 1))) && i > 4)
                    {
                        Board[i, j] = new CheckerCell
                        {
                            Row = i,
                            Column = j,
                            Checker = new BlackChecker(i, j),
                            Background = _colorOfUnselectedCell
                        };

                        continue;
                    }

                    if (((i % 2 == 0) && (j % 2 == 0)) || ((i % 2 == 1) && (j % 2 == 1)))
                    {
                        Board[i, j] = new CheckerCell
                        {
                            Row = i,
                            Column = j,
                            Background = _colorOfUnselectedCell
                        };

                        continue;
                    }

                    Board[i, j] = new CheckerCell
                    {
                        Row = i,
                        Column = j,
                        Background = Color.FromRgb(255, 255, 255)
                    };
                }
            }
        }

        private void SetWhiteKing()
        {
            if (_mainCell.Row == 7)
            {
                Board[_mainCell.Row, _mainCell.Column].Checker = new WhiteKing(_mainCell.Row, _mainCell.Column);
            }
        }

        private void SetBlackKing()
        {
            if (_mainCell.Row == 0)
            {
                Board[_mainCell.Row, _mainCell.Column].Checker = new BlackKing(_mainCell.Row, _mainCell.Column);
            }
        }

        private void RealizeAttack() 
        {
            _mustAtteck = null;
            _isHappened = true;

            if (_isFirstPlayer)
            {
                _countBlackCheckers--;

                OnPropertyChanged(nameof(CountBlackCheckers));
            }
            else
            {
                _countWhiteCheckers--;

                OnPropertyChanged(nameof(CountWhiteCheckers));
            }
        }

        private void PrepareAttackAfterAttack() 
        {
            OnBoardChanged();

            _mainMustAttack = _mainCell;
            _dependentCell = null;
            _isHappened = false;
        }

        private void Move() 
        {
            if (_mainCell.Checker is WhiteKing || _mainCell.Checker is BlackKing)
            {
                if (_isFirstPlayer)
                {
                    Board[_dependentCell.Row, _dependentCell.Column].Checker = new WhiteKing(_dependentCell.Row, _dependentCell.Column);
                }
                else
                {
                    Board[_dependentCell.Row, _dependentCell.Column].Checker = new BlackKing(_dependentCell.Row, _dependentCell.Column);
                }
            }
            else 
            {
                if (_isFirstPlayer)
                {
                    Board[_dependentCell.Row, _dependentCell.Column].Checker = new WhiteChecker(_dependentCell.Row, _dependentCell.Column);
                }
                else
                {
                    Board[_dependentCell.Row, _dependentCell.Column].Checker = new BlackChecker(_dependentCell.Row, _dependentCell.Column);
                }
            }
        }

        private void PrepareToFinishTurn() 
        {
            if (!(_mainCell.Checker is WhiteChecker || _mainCell.Checker is BlackKing))
            {
                if (_isFirstPlayer)
                {
                    SetWhiteKing();
                }
                else
                {
                    SetBlackKing();
                }
            }

            OnBoardChanged();

            _isFirstPlayer = !_isFirstPlayer;
            _mainCell = null;
            _dependentCell = null;
            _mainMustAttack = null;
            _isHappened = false;

            OnPropertyChanged(nameof(IsFirstPlayer));
        }

        private void PaintCell(ICell mainCell) //ToDo: add to new services
        {
            if (mainCell == null)
            {
                return;
            }

            SetPaintedCells(mainCell);

            if (mainCell.Background == _colorOfUnselectedCell)
            {
                mainCell.Background = _colorOfSelectedCell;

                foreach (var cell in _paintedCells) 
                {
                    cell.Background = _colorOfCellsToDoTurn;
                }
            }
            else 
            {
                mainCell.Background = _colorOfUnselectedCell;

                foreach (var cell in _paintedCells)
                {
                    cell.Background = _colorOfUnselectedCell;
                }

                _paintedCells = null;
            }
        }

        private void SetPaintedCells(ICell cellToPaint) //ToDo: add to new services
        {
            if (_paintedCells == null)
            {
                if (_mainCell.Checker is WhiteKing || _mainCell.Checker is BlackKing)
                {
                    _paintedCells = GetCellsToMainKingCellAttackOrMove(cellToPaint);
                }
                else
                {
                    _paintedCells = GetCellsToMainCheckerCellAttackOrMove(cellToPaint);
                }
            }
        }

        private void SetColor() //ToDo: add to new services
        {
            _colorOfCellsToDoTurn = Color.FromRgb(35, 135, 63);
            _colorOfSelectedCell = Color.FromRgb(93, 92, 99);
            _colorOfUnselectedCell = Color.FromRgb(0, 0, 0);
        }

        private void SetRowAndColumnDirection(out int rowDirection, out int columnDirection, ICell cellToGetDirection, ICell mainCell) //ToDo: add to new services
        {
            if (cellToGetDirection != null && mainCell.Row < cellToGetDirection.Row)
            {
                rowDirection = 1;
            }
            else if (cellToGetDirection != null && mainCell.Row > cellToGetDirection.Row)
            {
                rowDirection = -1;
            }
            else
            {
                rowDirection = 0;
            }

            if (cellToGetDirection != null && mainCell.Column < cellToGetDirection.Column)
            {
                columnDirection = 1;
            }
            else if (cellToGetDirection != null && mainCell.Column > cellToGetDirection.Column)
            {
                columnDirection = -1;
            }
            else
            {
                columnDirection = 0;
            }
        }

        private void SetCellsToMainKingCellAttack(List<ICell> cellsToAttack, ICell mainCell) //ToDo: add to new services
        {
            List<ICell> diagonal;
            bool foundCellToAttack = false;

            for (int i = 0; i < 4; i++)
            {
                diagonal = GetOneFromFourDiagonal(mainCell, i);

                if (diagonal == null)
                {
                    continue;
                }

                for (int j = 0; j < diagonal.Count; j++)
                {
                    if (diagonal[j].Checker != null
                        && diagonal[j].Checker.IsWhite != mainCell.Checker.IsWhite
                        && j + 1 != diagonal.Count
                        && !foundCellToAttack)
                    {
                        foundCellToAttack = true;

                        continue;
                    }

                    if (diagonal[j].Checker == null && foundCellToAttack)
                    {
                        cellsToAttack.Add(diagonal[j]);
                    }

                    if (diagonal[j].Checker != null && foundCellToAttack)
                    {
                        foundCellToAttack = false;

                        break;
                    }
                    else if (j + 1 == diagonal.Count)
                    {
                        foundCellToAttack = false;
                    }
                }
            }
        }

        private void SetCellsToMainKingCellMove(List<ICell> cellsToAttack, ICell mainCell) //ToDo: add to new services
        {
            List<ICell> diagonal;

            for (int i = 0; i < 4; i++)
            {
                diagonal = GetOneFromFourDiagonal(mainCell, i);

                if (diagonal == null)
                {
                    continue;
                }

                for (int j = 0; j < diagonal.Count; j++)
                {
                    if (diagonal[j].Checker == null)
                    {
                        cellsToAttack.Add(diagonal[j]);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void Reset() 
        {
            _isFirstPlayer = true;
            _isHappened = false;

            _countBlackCheckers = 12;
            _countWhiteCheckers = 12;
            _winnerMessage = null;

            SetColor();

            _mainCell = null;
            _dependentCell = null;
            _mainMustAttack = null;

            SetBoard();

            _mustAtteck = null;
            _paintedCells = null;

            OnBoardChanged();
            OnPropertyChanged(nameof(CountWhiteCheckers));
            OnPropertyChanged(nameof(CountBlackCheckers));
            OnPropertyChanged(nameof(IsFirstPlayer));
        }

        private bool CanAttack()
        {
            _mustAtteck = new List<ICell>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (_isFirstPlayer)
                    {
                        if (Board[i, j].Checker != null
                            && Board[i, j].Checker.IsWhite
                            && Board[i, j].Checker.CanAttack(Board))
                        {
                            _mustAtteck.Add(Board[i, j]);
                        }
                    }
                    else
                    {
                        if (Board[i, j].Checker != null
                            && !Board[i, j].Checker.IsWhite
                            && Board[i, j].Checker.CanAttack(Board))
                        {
                            _mustAtteck.Add(Board[i, j]);
                        }
                    }
                }
            }

            if (_mustAtteck.Count != 0)
            {
                return true;
            }

            _mustAtteck = null;

            return false;
        }

        private bool CanCreateCheckerDependencyCell(ICell dependencyCell)
        {
            if (_mainCell.Checker.CanAttack(Board))
            {
                if (Board[dependencyCell.Row, dependencyCell.Column].Checker == null
                    && (((dependencyCell.Row - 2 == _mainCell.Row) && (dependencyCell.Column - 2 == _mainCell.Column)
                        && Board[dependencyCell.Row - 1, dependencyCell.Column - 1].Checker != null
                        && Board[dependencyCell.Row - 1, dependencyCell.Column - 1].Checker.IsWhite != _mainCell.Checker.IsWhite)
                    || ((dependencyCell.Row + 2 == _mainCell.Row) && (dependencyCell.Column - 2 == _mainCell.Column)
                        && Board[dependencyCell.Row + 1, dependencyCell.Column - 1].Checker != null
                        && Board[dependencyCell.Row + 1, dependencyCell.Column - 1].Checker.IsWhite != _mainCell.Checker.IsWhite)
                    || ((dependencyCell.Row + 2 == _mainCell.Row) && (dependencyCell.Column + 2 == _mainCell.Column)
                        && Board[dependencyCell.Row + 1, dependencyCell.Column + 1].Checker != null
                        && Board[dependencyCell.Row + 1, dependencyCell.Column + 1].Checker.IsWhite != _mainCell.Checker.IsWhite)
                    || ((dependencyCell.Row - 2 == _mainCell.Row) && (dependencyCell.Column + 2 == _mainCell.Column)
                        && Board[dependencyCell.Row - 1, dependencyCell.Column + 1].Checker != null
                        && Board[dependencyCell.Row - 1, dependencyCell.Column + 1].Checker.IsWhite != _mainCell.Checker.IsWhite)))
                {
                    _dependentCell = Board[dependencyCell.Row, dependencyCell.Column];

                    return true;
                }

                return false;
            }
            else if (_mainCell.Checker.IsWhite
                    && Board[dependencyCell.Row, dependencyCell.Column].Checker == null
                    && dependencyCell.Row - 1 == _mainCell.Row
                    && (dependencyCell.Column - 1 == _mainCell.Column || dependencyCell.Column + 1 == _mainCell.Column))
            {
                _dependentCell = Board[dependencyCell.Row, dependencyCell.Column];

                return true;
            }
            else if (!_mainCell.Checker.IsWhite
                    && Board[dependencyCell.Row, dependencyCell.Column].Checker == null
                    && dependencyCell.Row + 1 == _mainCell.Row
                    && (dependencyCell.Column - 1 == _mainCell.Column || dependencyCell.Column + 1 == _mainCell.Column))
            {
                _dependentCell = Board[dependencyCell.Row, dependencyCell.Column];

                return true;
            }

            return false;
        }

        private bool CanCreateKingDependencyCell(ICell dependencyCell)
        {
            if (Board[dependencyCell.Row, dependencyCell.Column].Checker != null)
            {
                return false;
            }

            List<ICell> diagonal = GetDiagonal(dependencyCell);

            if (_mainCell.Checker.CanAttack(Board))
            {
                if (!diagonal.Contains(Board[dependencyCell.Row, dependencyCell.Column]))
                {
                    return false;
                }

                bool haveCellToAttack = false;

                for (int i = 0; i < diagonal.IndexOf(Board[dependencyCell.Row, dependencyCell.Column]); i++)
                {
                    if (diagonal[i].Checker != null)
                    {
                        if (diagonal[i].Checker.IsWhite == _mainCell.Checker.IsWhite)
                        {
                            return false;
                        }
                        else if (diagonal[i].Checker.IsWhite != _mainCell.Checker.IsWhite
                                && !haveCellToAttack)
                        {
                            haveCellToAttack = true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                if (haveCellToAttack)
                {
                    _dependentCell = Board[dependencyCell.Row, dependencyCell.Column];

                    return true;
                }
            }
            else
            {
                for (int i = 0; i < diagonal.IndexOf(Board[dependencyCell.Row, dependencyCell.Column]); i++)
                {
                    if (diagonal[i].Checker != null)
                    {
                        return false;
                    }
                }

                _dependentCell = Board[dependencyCell.Row, dependencyCell.Column];

                return true;
            }

            return false;
        }

        private bool CanCreateDependencyCell(ICell dependencyCell) 
        {
            if (_mainCell.Checker is WhiteKing || _mainCell.Checker is BlackKing 
                && CanCreateKingDependencyCell(dependencyCell))
            {
                return true;
            }
            else if (CanCreateCheckerDependencyCell(dependencyCell))
            {
                return true;
            }

            return false;
        }

        private bool SetDependencyCell(ICell dependencyCell)
        {
            if (_mainCell != null
                && (_mainMustAttack == _mainCell || _mainMustAttack == null)
                && CanCreateDependencyCell(dependencyCell))
            {
                if (_mainCell.Checker.CanAttack(Board))
                {
                    GetCellToAttack(dependencyCell.Row, dependencyCell.Column, _mainCell).Checker = null;
         
                    RealizeAttack();
                }

                Move();

                Board[_mainCell.Row, _mainCell.Column].Checker = null;

                PaintCell(Board[_mainCell.Row, _mainCell.Column]);

                _mainCell = Board[_dependentCell.Row, _dependentCell.Column];

                if (_mainCell.Checker.CanAttack(Board) && _isHappened)
                {
                    PaintCell(_mainCell);
                    PrepareAttackAfterAttack();

                    return true;
                }

                PrepareToFinishTurn();
            }
            else if(_mainMustAttack == null)
            {
                PaintCell(_mainCell);
                OnBoardChanged();

                _mainCell = null;
            }

            return false;
        }

        private bool SetMainCell(ICell dependencyCell) 
        {
            if (_mainCell == null && CanAttack())
            {
                for (int i = 0; i < _mustAtteck.Count; i++)
                {
                    if (Board[dependencyCell.Row, dependencyCell.Column] == _mustAtteck[i])
                    {
                        _mainCell = _mustAtteck[i];

                        PaintCell(_mainCell);
                        OnBoardChanged();

                        return true;
                    }
                }

                return true;
            }

            if (_mainMustAttack == null && _mainCell == null
                    && Board[dependencyCell.Row, dependencyCell.Column].Checker != null
                    && Board[dependencyCell.Row, dependencyCell.Column].Checker.CanMove(Board))
            {
                if (_isFirstPlayer && Board[dependencyCell.Row, dependencyCell.Column].Checker.IsWhite)
                {
                    _mainCell = Board[dependencyCell.Row, dependencyCell.Column];

                    PaintCell(_mainCell);
                    OnBoardChanged();

                    return true;
                }
                else if(!_isFirstPlayer && !Board[dependencyCell.Row, dependencyCell.Column].Checker.IsWhite) 
                {
                    _mainCell = Board[dependencyCell.Row, dependencyCell.Column];

                    PaintCell(_mainCell);
                    OnBoardChanged();

                    return true;
                }
            }

            return false;
        }

        private bool CanBeAttackedByMainCell(int rowDirection, int columnDirection) //ToDo: add to new services
        {
            return _mainCell.Row + rowDirection * 2 < Size && _mainCell.Row + rowDirection * 2 > -1
                    && _mainCell.Column + columnDirection * 2 < Size && _mainCell.Column + columnDirection * 2 > -1
                    && Board[_mainCell.Row + rowDirection * 2, _mainCell.Column + columnDirection * 2].Checker == null
                    && Board[_mainCell.Row + rowDirection, _mainCell.Column + columnDirection].Checker != null
                    && Board[_mainCell.Row + rowDirection, _mainCell.Column + columnDirection].Checker.IsWhite != _mainCell.Checker.IsWhite;
        }

        private bool HaveWinner() 
        {
            if (_countWhiteCheckers < _countBlackCheckers)
            {
                if (_countWhiteCheckers == 0)
                {
                    WinnerMessage = "Black checker is winner";

                    return true;
                }

                if (GetLoserCells().Count == _countWhiteCheckers)
                {
                    WinnerMessage = "Black checker is winner";

                    return true;
                }
            }
            else if (_countBlackCheckers < _countWhiteCheckers)
            {
                if (_countBlackCheckers == 0)
                {
                    WinnerMessage = "White checker is winner";

                    return true;
                }

                if (GetLoserCells().Count == _countBlackCheckers)
                {
                    WinnerMessage = "White checker is winner";

                    return true;
                }
            }

            return false;
        }

        private bool IsLoserCell(ICell cell, bool IsWhite) 
        {
            return cell.Checker != null
                        && IsWhite
                        && !cell.Checker.CanAttack(Board)
                        && !cell.Checker.CanMove(Board);
        }

        private ICell GetCellToAttack(int rowDependenceCell, int columnDependenceCell, ICell mainCell) 
        {
            if (_mainCell.Checker is WhiteKing || _mainCell.Checker is BlackKing)
            {
                return GetCellToKingAttack(rowDependenceCell, columnDependenceCell, mainCell);
            }
            else 
            {
                return GetCellBetweenMainDependency();
            }
        }

        private ICell GetCellBetweenMainDependency() 
        {
            int row;
            int column;

            if (_dependentCell.Row > _mainCell.Row)
            {
                row = _dependentCell.Row - 1;
            }
            else 
            {
                row = _dependentCell.Row + 1;
            }

            if (_dependentCell.Column > _mainCell.Column)
            {
                column = _dependentCell.Column - 1;
            }
            else 
            {
                column = _dependentCell.Column + 1;
            }

            return Board[row, column];
        }

        private ICell GetCellToKingAttack(int rowDependenceCell, int columnDependenceCell, ICell mainCell) 
        {
            if (mainCell == null)
            {
                return null;
            }

            SetRowAndColumnDirection(out int rowDirection, out int columnDirection, _dependentCell, mainCell);

            for (int i = mainCell.Row + rowDirection, j = mainCell.Column + columnDirection; 
                    (i < 8 && i > -1) && (j < 8 && j > -1); i += rowDirection, j += columnDirection)
            {
                if (Board[i, j].Checker != null)
                {
                    if (Board[i, j].Checker.IsWhite == mainCell.Checker.IsWhite)
                    {
                        return null;
                    }
                    else 
                    {
                        return Board[i, j];
                    }
                }
            }

            return null;
        }

        private List<ICell> GetDiagonal(ICell cellToGetDirection) //ToDo: add to new services
        {
            List<ICell> diagonal = new List<ICell>();

            SetRowAndColumnDirection(out int rowDirection, out int columnDirection, cellToGetDirection, _mainCell);

            if (rowDirection == 0 || columnDirection == 0)
            {
                return diagonal;
            }

            for (int i = _mainCell.Row + rowDirection, j = _mainCell.Column + columnDirection; 
                    (i < 8 && i > -1) && (j < 8 && j > -1); i += rowDirection, j += columnDirection)
            {
                diagonal.Add(Board[i, j]);
            }

            return diagonal;
        }

        private List<ICell> GetCellsToMainCheckerCellAttackOrMove(ICell mainCell) //ToDo: add to new services
        {
            List<ICell> cellsToAttack = new List<ICell>();

            if (mainCell.Checker.CanAttack(Board))
            {
                if (CanBeAttackedByMainCell(1, 1))
                {
                    cellsToAttack.Add(Board[mainCell.Row + 2, mainCell.Column + 2]);
                }

                if (CanBeAttackedByMainCell(1, -1))
                {
                    cellsToAttack.Add(Board[mainCell.Row + 2, mainCell.Column - 2]);
                }

                if (CanBeAttackedByMainCell(-1, -1))
                {
                    cellsToAttack.Add(Board[mainCell.Row - 2, mainCell.Column - 2]);
                }

                if (CanBeAttackedByMainCell(-1, 1))
                {
                    cellsToAttack.Add(Board[mainCell.Row - 2, mainCell.Column + 2]);
                }
            }
            else 
            {
                if (mainCell.Column - 1 > - 1)
                {
                    if (mainCell.Checker.IsWhite 
                        && mainCell.Row + 1 < Size 
                        && Board[mainCell.Row + 1, mainCell.Column - 1].Checker == null)
                    {
                        cellsToAttack.Add(Board[mainCell.Row + 1, mainCell.Column - 1]);
                    }

                    if (!mainCell.Checker.IsWhite 
                        && mainCell.Row - 1 > -1
                        && Board[mainCell.Row - 1, mainCell.Column - 1].Checker == null)
                    {
                        cellsToAttack.Add(Board[mainCell.Row - 1, mainCell.Column - 1]);
                    }
                }

                if (mainCell.Column + 1 < Size)
                {
                    if (mainCell.Checker.IsWhite 
                        && mainCell.Row + 1 < Size
                        && Board[mainCell.Row + 1, mainCell.Column + 1].Checker == null)
                    {
                        cellsToAttack.Add(Board[mainCell.Row + 1, mainCell.Column + 1]);
                    }

                    if (!mainCell.Checker.IsWhite 
                        && mainCell.Row - 1 > -1
                        && Board[mainCell.Row - 1, mainCell.Column + 1].Checker == null)
                    {
                        cellsToAttack.Add(Board[mainCell.Row - 1, mainCell.Column + 1]);
                    }
                }
            }

            return cellsToAttack;
        }

        private List<ICell> GetCellsToMainKingCellAttackOrMove(ICell mainCell) //ToDo: add to new services
        {
            List<ICell> cellsToAttackOrMove = new List<ICell>();

            if (mainCell.Checker.CanAttack(Board))
            {
                SetCellsToMainKingCellAttack(cellsToAttackOrMove, mainCell);
            }
            else 
            {
                SetCellsToMainKingCellMove(cellsToAttackOrMove, mainCell);
            }

            return cellsToAttackOrMove;
        }

        private List<ICell> GetOneFromFourDiagonal(ICell cellToGetDiagonal, int numberOfDiagonal) //ToDo: add to new services
        {
            switch (numberOfDiagonal) 
            {
                case 0:
                    if (cellToGetDiagonal.Row - 1 > -1 && cellToGetDiagonal.Column - 1 > -1)
                    {
                        return GetDiagonal(Board[cellToGetDiagonal.Row - 1, cellToGetDiagonal.Column - 1]);
                    }
                    break;
                case 1:
                    if (cellToGetDiagonal.Row - 1 > -1 && cellToGetDiagonal.Column + 1 < Size)
                    {
                        return GetDiagonal(Board[cellToGetDiagonal.Row - 1, cellToGetDiagonal.Column + 1]);
                    }
                    break;
                case 2:
                    if (cellToGetDiagonal.Row + 1 < Size && cellToGetDiagonal.Column + 1 < Size)
                    {
                        return GetDiagonal(Board[cellToGetDiagonal.Row + 1, cellToGetDiagonal.Column + 1]);
                    }                    
                    break;
                case 3:
                    if (cellToGetDiagonal.Row + 1 < Size && cellToGetDiagonal.Column - 1 > -1)
                    {
                        return GetDiagonal(Board[cellToGetDiagonal.Row + 1, cellToGetDiagonal.Column - 1]);
                    }                    
                    break;
            }

            return null;
        }

        private List<ICell> GetLoserCells()
        {
            List<ICell> loserCells = new List<ICell>();

            if (_countWhiteCheckers < _countBlackCheckers)
            {
                foreach (ICell cell in Board)
                {
                    if (cell.Checker != null && IsLoserCell(cell, cell.Checker.IsWhite))
                    {
                        loserCells.Add(cell);
                    }
                }
            }
            else if (_countBlackCheckers < _countWhiteCheckers)
            {
                foreach (ICell cell in Board)
                {
                    if (cell.Checker != null && IsLoserCell(cell, !cell.Checker.IsWhite))
                    {
                        loserCells.Add(cell);
                    }
                }
            }

            return loserCells;
        }
    }
}
