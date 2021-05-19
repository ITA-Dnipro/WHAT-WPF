using Checkers.Models.Interfaces;

namespace Checkers.Models.Abstracts
{
    internal abstract class Figure : IFigure
    {
        protected int _row;
        protected int _column;

        public virtual string TypeChecker { get; protected set; }

        public virtual bool IsWhite { get; protected set; }

        public abstract bool CanAttack(ICell[,] board);

        public abstract bool CanMove(ICell[,] board);
    }
}
