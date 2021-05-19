using Checkers.Models.Abstracts;
using Checkers.Models.Interfaces;

namespace Checkers.Models
{
    internal class BlackChecker : Checker
    {
        public BlackChecker(int row, int column) : base(row, column)
        {
            TypeChecker = "../../Icons/BlackChecker.svg";
            IsWhite = false;
        }

        public override bool CanMove(ICell[,] board)
        {
            if (_row == 0)
            {
                return false;
            }

            switch (_column)
            {
                case 0:
                    if (board[_row - 1, _column + 1].Checker == null)
                    {
                        return true;
                    }
                    break;
                case 7:
                    if (board[_row - 1, _column - 1].Checker == null)
                    {
                        return true;
                    }
                    break;
                default:
                    if ((_row - 1 > -1 && _column + 1 < 8 && board[_row - 1, _column + 1].Checker == null)
                        || (_row - 1 > -1 && _column - 1 > -1 && board[_row - 1, _column - 1].Checker == null))
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }
    }
}
