using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tetris.Model.Shape
{
    class ShapeO : BaseShape
    {
        public override void Create(int startX, int startY)
        {
            startY--;

            Points.Add(new Coordinate(startX, startY));
            Points.Add(new Coordinate(startX, startY + 1));
            Points.Add(new Coordinate(startX + 1, startY));
            Points.Add(new Coordinate(startX + 1, startY + 1));
            SetColor(Colors.Yellow);
        }
    }
}
