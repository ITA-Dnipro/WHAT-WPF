using Checkers.Models.Interfaces;

namespace Checkers.Models
{
    public class BlackKing : IChecker
    {
        private int _row;
        private int _column;
        private ICell[,] _board;

        public string TypeChecker { get; }

        public bool IsWhite => false;

        public bool CanMove 
        {
            get 
            {
                if ((_row - 1 > -1 && _column - 1 > -1 && _board[_row - 1, _column - 1].Checker == null)
                    || (_row + 1 < 7 && _column - 1 > -1 && _board[_row + 1, _column - 1].Checker == null)
                    || (_row + 1 < 7 && _column + 1 < 7 && _board[_row + 1, _column + 1].Checker == null)
                    || (_row - 1 > -1 && _column + 1 < 7 && _board[_row - 1, _column + 1].Checker == null))
                {
                    return true;
                }

                return false;
            }
        }

        public bool CanAttack 
        {
            get 
            {
                if (_row - 1 > -1 && _column - 1 > -1)
                {
                    ICell cellToAttack = GetCellToAttack(-1, -1);

                    if (cellToAttack != null && cellToAttack.Row - 1 > -1 && cellToAttack.Column - 1 > -1
                        && _board[cellToAttack.Row - 1, cellToAttack.Column - 1].Checker == null)
                    {
                        return true;
                    }
                }

                if (_row + 1 < 8 && _column - 1 > -1)
                {
                    ICell cellToAttack = GetCellToAttack(1, -1);

                    if (cellToAttack != null && cellToAttack.Row + 1 < 7 && cellToAttack.Column - 1 > -1
                        && _board[cellToAttack.Row + 1, cellToAttack.Column - 1].Checker == null)
                    {
                        return true;
                    }
                }

                if (_row + 1 < 8 && _column + 1 < 8)
                {
                    ICell cellToAttack = GetCellToAttack(1, 1);

                    if (cellToAttack != null && cellToAttack.Row + 1 < 8 && cellToAttack.Column + 1 < 8
                        && _board[cellToAttack.Row + 1, cellToAttack.Column + 1].Checker == null)
                    {
                        return true;
                    }
                }

                if (_row - 1 > -1 && _column + 1 < 8)
                {
                    ICell cellToAttack = GetCellToAttack(-1, 1);

                    if (cellToAttack != null && cellToAttack.Row - 1 > -1 && cellToAttack.Column + 1 < 8
                        && _board[cellToAttack.Row - 1, cellToAttack.Column + 1].Checker == null)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public BlackKing(int row, int column, ICell[,] board) 
        {
            TypeChecker = "../../Icons/BlackKing.svg";

            _row = row;
            _column = column;
            _board = board;
        }

        private ICell GetCellToAttack(int rowDirection, int columnDirection)
        {
            for (int i = _row + rowDirection, j = _column + columnDirection; 
                    (i < 8 && i > -1) && (j < 8 && j > -1); i += rowDirection, j += columnDirection)
            {
                if (_board[i, j].Checker != null)
                {
                    if (!_board[i, j].Checker.IsWhite)
                    {
                        return null;
                    }
                    else
                    {
                        return _board[i, j];
                    }
                }
            }

            return null;
        }
    }
}
