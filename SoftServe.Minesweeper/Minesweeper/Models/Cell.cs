using Minesweeper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models
{
    public class Cell : NotifyPropertyChanged
    {
        private bool isHidden;
        private bool isMined;
        private bool isFlaged = false;
        private HiddenCellColor color;
        private ContentInCell openContent;
        private int x;
        private int y;

        public bool IsHidden
        {
            get => isHidden;
            set
            {
                isHidden = value;
                OnPropertyChanged();
            }
        }

        public bool IsMined { get => isMined; }

        public bool IsFlaged
        {
            get => isFlaged;
            set
            {
                isFlaged = value;
                OnPropertyChanged();
            }
        }

        public HiddenCellColor Color { get => color; }

        public ContentInCell OpenContent
        {
            get => openContent;
            set
            {
                openContent = value;
                OnPropertyChanged();
            }
        }

        public int X => x;
        public int Y => y;

        public Cell(bool isMined, HiddenCellColor color, int x, int y)
        {
            this.isMined = isMined;
            this.color = color;
            isHidden = true;
            this.x = x;
            this.y = y;
        }
    }
}
