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
        public override void Create(int startY, int startX)
        {
            startY--;

            Points.Add(new Coordinate(startY, startX));
            Points.Add(new Coordinate(startY + 1, startX));
            Points.Add(new Coordinate(startY, startX + 1));
            Points.Add(new Coordinate(startY + 1, startX + 1));
            SetColor(Colors.Yellow);
        }
    }
}
