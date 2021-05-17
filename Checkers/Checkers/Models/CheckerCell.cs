using System.Windows.Media;

using Checkers.Models.Interfaces;

namespace Checkers.Models
{
    internal class CheckerCell : ICell
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public IChecker Checker { get; set; }

        public Color Background { get; set; }
    }
}
