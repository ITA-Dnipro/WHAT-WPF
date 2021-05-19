using Checkers.Models.Abstracts;

namespace Checkers.Models
{
    internal class BlackKing : CheckerKing
    {
        public BlackKing(int row, int column) : base(row, column)
        {
            TypeChecker = "../../Icons/BlackKing.svg";
            IsWhite = false;
        }
    }
}
