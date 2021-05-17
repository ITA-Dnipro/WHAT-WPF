using Checkers.Models.Interfaces;

namespace Checkers.Models.Abstracts
{
    internal abstract class Checker : IFigure
    {
        protected int _row;
        protected int _column;

        public virtual bool IsWhite { get; protected set; }

        public virtual string TypeChecker { get; protected set; }

        public Checker(int row, int column) 
        {
            _row = row;
            _column = column;
        }

        public virtual bool CanAttack(ICell[,] board)
        {
            if (_row < 2 && CanAttackIfRowLessTwo(board))
            {
                return true;
            }

            if (_row > 5 && CanAttackIfRowMoreFive(board))
            {
                return true;
            }

            if (_row > 1 && _row < 6 && CanAttackIfRowMoreOneAndLessSix(board))
            {
                return true;
            }

            return false;
        }

        public abstract bool CanMove(ICell[,] board);

        private bool CanAttackAtRightBottom(ICell[,] board) 
        {
            return board[_row + 1, _column + 1].Checker != null
                    && board[_row + 1, _column + 1].Checker.IsWhite != IsWhite
                    && board[_row + 2, _column + 2].Checker == null;
        }

        private bool CanAttackAtLeftBottom(ICell[,] board) 
        {
            return board[_row + 1, _column - 1].Checker != null
                    && board[_row + 1, _column - 1].Checker.IsWhite != IsWhite
                    && board[_row + 2, _column - 2].Checker == null;
        }

        private bool CanAttackAtRightTop(ICell[,] board) 
        {
            return board[_row - 1, _column + 1].Checker != null
                    && board[_row - 1, _column + 1].Checker.IsWhite != IsWhite
                    && board[_row - 2, _column + 2].Checker == null;
        }

        private bool CanAttackAtLeftTop(ICell[,] board) 
        {
            return board[_row - 1, _column - 1].Checker != null
                    && board[_row - 1, _column - 1].Checker.IsWhite != IsWhite
                    && board[_row - 2, _column - 2].Checker == null;
        }

        private bool CanAttackIfRowLessTwo(ICell[,] board) 
        {
            if (_column < 2 && CanAttackAtRightBottom(board))
            {
                return true;
            }

            if (_column > 5 && CanAttackAtLeftBottom(board))
            {
                return true;
            }

            if (_column > 1 && _column < 6 && (CanAttackAtRightBottom(board) || CanAttackAtLeftBottom(board)))
            {
                return true;
            }

            return false;
        }

        private bool CanAttackIfRowMoreFive(ICell[,] board) 
        {
            if (_column < 2 && CanAttackAtRightTop(board))
            {
                return true;
            }

            if (_column > 5 && CanAttackAtLeftTop(board))
            {
                return true;
            }

            if (_column > 1 && _column < 6 && (CanAttackAtRightTop(board) || CanAttackAtLeftTop(board)))
            {
                return true;
            }

            return false;
        }

        private bool CanAttackIfRowMoreOneAndLessSix(ICell[,] board) 
        {
            if (_column < 2 && (CanAttackAtRightBottom(board) || CanAttackAtRightTop(board)))
            {
                return true;
            }

            if (_column > 5 && (CanAttackAtLeftTop(board) || CanAttackAtLeftBottom(board)))
            {
                return true;
            }

            if (_column > 1 && _column < 6
                && (CanAttackAtRightBottom(board) || CanAttackAtLeftBottom(board) || CanAttackAtRightTop(board) || CanAttackAtLeftTop(board)))
            {
                return true;
            }

            return false;
        }
    }
}
