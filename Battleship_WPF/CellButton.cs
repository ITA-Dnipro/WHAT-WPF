using BattleshipLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Battleship_WPF
{
    class CellButton : Button
    {
        public Position Coord { get; set; }
    }
}
