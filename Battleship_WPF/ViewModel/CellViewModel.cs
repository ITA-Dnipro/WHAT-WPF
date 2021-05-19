
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
    public class CellViewModel : INotifyPropertyChanged
    {
        private Position _coord;
        private string _styleKey;

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

        public CellViewModel(Position coord, string styleKey)
        {
            _coord = coord;
            _styleKey = styleKey;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
