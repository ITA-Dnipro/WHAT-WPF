using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestButtons.ViewModel;
using TestButtons;

namespace TestButtons.Model
{
    public class CellViewModel: INotifyPropertyChanged
    {
        private static bool isX = true;
        private MarkType? markTypes;

        public MarkType? MarkTypes
        { 
            get => markTypes;
            set
            {
                markTypes = value;
                OnPropertyChanged(nameof(MarkTypes));
            }
        }
        public int Row { get; set; }
        public int Column { get; set; }

        public CellViewModel()
        {
            MarkTypes = null;
        }

        public static void SetIsX(bool _isX)
        {
            isX = _isX;
        }

        public void GetMarkType()
        {
            if (MarkTypes == null)
            {
                if (isX)
                {
                    MarkTypes = MarkType.X;
                }
                else
                {
                    MarkTypes = MarkType.O;
                }
                isX = !isX;
            }
        }

        public void ClearMarkType()
        {
            MarkTypes = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
