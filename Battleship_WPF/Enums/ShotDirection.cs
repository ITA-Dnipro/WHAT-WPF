using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_WPF
{
    [Flags]
    public enum ShotDirection
    {
        NoneDirection = 0,
        TwoCellWestNorth = 1,
        FourCellWestNorth = 2,
        SixCellWestNorth = 4,
        EightCellWestNorth = 8,
        Center = 16,
        EightCellEastSouth = 32,
        SixCellEastSouth = 64,
        FourCellEastSouth = 128,
        TwoCellEastSouth = 256
    }
}
