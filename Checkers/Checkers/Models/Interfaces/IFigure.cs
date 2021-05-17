using Checkers.Models.Interfaces;

namespace Checkers.Models.Interfaces
{
    public interface IFigure
    {
        string TypeChecker { get; }

        bool IsWhite { get; }

        bool CanMove(ICell[,] board);

        bool CanAttack(ICell[,] board);
    }
}
