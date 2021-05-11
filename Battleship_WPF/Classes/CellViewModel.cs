using BattleshipLibrary;
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
        private string _imagePath;

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

        public string ImagePath
        {
            get
            {
                return _imagePath;
            }
            set
            {
                _imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }

        
        public CellViewModel(Position coord, string imagePath)
        {
            _coord = coord;
            _imagePath = imagePath;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
