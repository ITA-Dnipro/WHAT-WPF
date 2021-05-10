using BattleshipLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Battleship_WPF
{
    public class CellViewModel
    {
        public Position Coord { get; set; }

        public string ImagePath { get; set; }

        public CellViewModel(Position coord)
        {
            Coord = coord;
        }
    }
}
