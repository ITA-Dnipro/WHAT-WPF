using System.Windows.Media;

namespace Checkers.Models.Interfaces
{
    public interface ICell
    {
        int Row { get; set; }

        int Column { get; set; }
        
        IFigure Checker { get; set; }
        
        Color Background { get; set; }
    }
}
