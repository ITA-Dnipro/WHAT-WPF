using Checkers.Models.Interfaces;

namespace Checkers.Models.Abstracts
{
    internal class CheckerKing : Figure
    {
        public CheckerKing(int row, int column) 
        {
            _row = row;
            _column = column;
        }

        public override bool CanMove(ICell[,] board)
        {
            if ((_row - 1 > -1 && _column - 1 > -1 && board[_row - 1, _column - 1].Checker == null)
                || (_row + 1 < 7 && _column - 1 > -1 && board[_row + 1, _column - 1].Checker == null)
                || (_row + 1 < 7 && _column + 1 < 7 && board[_row + 1, _column + 1].Checker == null)
                || (_row - 1 > -1 && _column + 1 < 7 && board[_row - 1, _column + 1].Checker == null))
            {
                return true;
            }

            return false;
        }

        public override bool CanAttack(ICell[,] board)
        {
            if (_row - 1 > -1 && _column - 1 > -1)
            {
                ICell cellToAttack = GetCellToAttack(-1, -1, board);

                if (cellToAttack != null && cellToAttack.Row - 1 > -1 && cellToAttack.Column - 1 > -1
                    && board[cellToAttack.Row - 1, cellToAttack.Column - 1].Checker == null)
                {
                    return true;
                }
            }

            if (_row + 1 < 8 && _column - 1 > -1)
            {
                ICell cellToAttack = GetCellToAttack(1, -1, board);

                if (cellToAttack != null && cellToAttack.Row + 1 < 7 && cellToAttack.Column - 1 > -1
                    && board[cellToAttack.Row + 1, cellToAttack.Column - 1].Checker == null)
                {
                    return true;
                }
            }

            if (_row + 1 < 8 && _column + 1 < 8)
            {
                ICell cellToAttack = GetCellToAttack(1, 1, board);

                if (cellToAttack != null && cellToAttack.Row + 1 < 8 && cellToAttack.Column + 1 < 8
                    && board[cellToAttack.Row + 1, cellToAttack.Column + 1].Checker == null)
                {
                    return true;
                }
            }

            if (_row - 1 > -1 && _column + 1 < 8)
            {
                ICell cellToAttack = GetCellToAttack(-1, 1, board);

                if (cellToAttack != null && cellToAttack.Row - 1 > -1 && cellToAttack.Column + 1 < 8
                    && board[cellToAttack.Row - 1, cellToAttack.Column + 1].Checker == null)
                {
                    return true;
                }
            }

            return false;
        }

        protected ICell GetCellToAttack(int rowDirection, int columnDirection, ICell[,] board)
        {
            for (int i = _row + rowDirection, j = _column + columnDirection;
                    (i < 8 && i > -1) && (j < 8 && j > -1); i += rowDirection, j += columnDirection)
            {
                if (board[i, j].Checker != null)
                {
                    if (board[i, j].Checker.IsWhite == IsWhite)
                    {
                        return null;
                    }
                    else
                    {
                        return board[i, j];
                    }
                }
            }

            return null;
        }
    }
}
