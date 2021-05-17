using Checkers.Models.Interfaces;

namespace Checkers.Models
{
    internal class WhiteChecker : IChecker
    {
        private int _row;
        private int _column;
        private ICell[,] _board;

        public string TypeChecker { get; }

        public bool IsWhite => true;

        public bool CanMove 
        {
            get
            {
                if (_row == 7)
                {
                    return false;
                }

                switch (_column) 
                {
                    case 0:
                        if (_board[_row + 1, _column + 1].Checker == null)
                        {
                            return true;
                        }
                        break;
                    case 7:
                        if (_board[_row + 1, _column - 1].Checker == null)
                        {
                            return true;
                        }
                        break;
                    default:
                        if ((_row + 1 < 8 && _column + 1 < 8 && _board[_row + 1, _column + 1].Checker == null)
                            || (_row + 1 < 8 && _column - 1 > -1 && _board[_row + 1, _column - 1].Checker == null))
                        {
                            return true;
                        }
                        break;
                }

                return false;
            }
        }

        public bool CanAttack 
        {
            get 
            {
                if (_row < 2)
                {
                    if (_column < 2
                        && _board[_row + 1, _column + 1].Checker != null
                        && !_board[_row + 1, _column + 1].Checker.IsWhite
                        && _board[_row + 2, _column + 2].Checker == null)
                    {
                        return true;
                    }

                    if (_column > 5
                        && _board[_row + 1, _column - 1].Checker != null
                        && !_board[_row + 1, _column - 1].Checker.IsWhite
                        && _board[_row + 2, _column - 2].Checker == null)
                    {
                        return true;
                    }

                    if (_column > 1 && _column < 6
                        && ((_board[_row + 1, _column + 1].Checker != null 
                            && !_board[_row + 1, _column + 1].Checker.IsWhite && _board[_row + 2, _column + 2].Checker == null)
                        || (_board[_row + 1, _column - 1].Checker != null 
                            && !_board[_row + 1, _column - 1].Checker.IsWhite && _board[_row + 2, _column - 2].Checker == null)))
                    {
                        return true;
                    }
                }

                if (_row > 5)
                {
                    if (_column < 2
                        && _board[_row - 1, _column + 1].Checker != null
                        && !_board[_row - 1, _column + 1].Checker.IsWhite
                        && _board[_row - 2, _column + 2].Checker == null)
                    {
                        return true;
                    }

                    if (_column > 5
                        && _board[_row - 1, _column - 1].Checker != null
                        && !_board[_row - 1, _column - 1].Checker.IsWhite
                        && _board[_row - 2, _column - 2].Checker == null)
                    {
                        return true;
                    }

                    if (_column > 1 && _column < 6
                        && ((_board[_row - 1, _column + 1].Checker != null 
                            && !_board[_row - 1, _column + 1].Checker.IsWhite && _board[_row - 2, _column + 2].Checker == null)
                        || (_board[_row - 1, _column - 1].Checker != null 
                            && !_board[_row - 1, _column - 1].Checker.IsWhite && _board[_row - 2, _column - 2].Checker == null)))
                    {
                        return true;
                    }
                }

                if (_row > 1 && _row < 6)
                {
                    if (_column < 2
                        && ((_board[_row + 1, _column + 1].Checker != null 
                            && !_board[_row + 1, _column + 1].Checker.IsWhite && _board[_row + 2, _column + 2].Checker == null)
                        || (_board[_row - 1, _column + 1].Checker != null 
                            && !_board[_row - 1, _column + 1].Checker.IsWhite && _board[_row - 2, _column + 2].Checker == null)))
                    {
                        return true;
                    }

                    if (_column > 5
                        && ((_board[_row - 1, _column - 1].Checker != null 
                            && !_board[_row - 1, _column - 1].Checker.IsWhite && _board[_row - 2, _column - 2].Checker == null)
                        ||(_board[_row + 1, _column - 1].Checker != null 
                            && !_board[_row + 1, _column - 1].Checker.IsWhite && _board[_row + 2, _column - 2].Checker == null)))
                    {
                        return true;
                    }

                    if (_column > 1 && _column < 6
                        &&((_board[_row + 1, _column + 1].Checker != null 
                            && !_board[_row + 1, _column + 1].Checker.IsWhite && _board[_row + 2, _column + 2].Checker == null)
                        ||(_board[_row + 1, _column - 1].Checker != null 
                            && !_board[_row + 1, _column - 1].Checker.IsWhite && _board[_row + 2, _column - 2].Checker == null)
                        ||(_board[_row - 1, _column + 1].Checker != null 
                            && !_board[_row - 1, _column + 1].Checker.IsWhite && _board[_row - 2, _column + 2].Checker == null)
                        ||(_board[_row - 1, _column - 1].Checker != null 
                            && !_board[_row - 1, _column - 1].Checker.IsWhite && _board[_row - 2, _column - 2].Checker == null)))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public WhiteChecker(int row, int column, ICell[,] board) 
        {
            TypeChecker = "../../Icons/WhiteChecker.svg";

            _row = row;
            _column = column;
            _board = board;
        }
    }
}
