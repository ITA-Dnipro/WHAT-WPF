using Checkers.Models.Abstracts;

namespace Checkers.Models
{
    internal class WhiteKing : CheckerKing
    {
        public WhiteKing(int row, int column) : base(row, column)
        {
            TypeChecker = "../../Icons/WhiteKing.svg";
            IsWhite = true;
        }
    }
}
