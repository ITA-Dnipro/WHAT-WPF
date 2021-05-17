using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Enums
{
    public enum ContentInCell
    {
        Empty,
        OneBombNear,
        TwoBombNear,
        ThreeBombNear,
        FourBombNear,
        FiveBombNear,
        SixBombNear,
        SevenBombNear,
        EightBombNear,
        Bomb,
        Flag
    }
}
