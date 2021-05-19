namespace Checkers.Models.Interfaces
{
    public interface IChecker
    {
        string TypeChecker { get; }

        bool CanMove { get; }        

        bool CanAttack { get; }

        bool IsWhite { get; }
    }
}
