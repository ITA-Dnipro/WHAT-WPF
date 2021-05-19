using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship_WPF
{
    [Flags]
    public enum BitShipType : byte
    {
        NoneDeck = 0,
        OneDeckShipCount = 1,
        TwoDeckShipCount = 2,
        ThreeDeckShipCount = 4,
        FourDeckShipCount = 8
    }
}
