
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Battleship_WPF
{
    public class CellViewModel : BaseViewModel
    {
        private Position _coord;
        private string _styleKey;

        public CellViewModel(Position coord, string styleKey)
        {
            _coord = coord;
            _styleKey = styleKey;
        }

        public Position Coord
        {
            get
            {
                return _coord;
            }
            set
            {
                _coord = value;
                OnPropertyChanged("Coord");
            }
        }

        public string StyleKey
        {
            get
            {
                return _styleKey;
            }
            set
            {
                _styleKey = value;
                OnPropertyChanged("StyleKey");
            }
        }
    }
}
