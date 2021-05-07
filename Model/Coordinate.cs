using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tetris.Model
{
    class Coordinate : ICloneable
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coordinate(int x, int y, Color pointColor)
        {
            X = x;
            Y = y;
            PointColor = pointColor;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Color PointColor { get; set; }

        public object Clone()
        {
            return new Coordinate(this.X, this.Y, this.PointColor);
        }
    }
}
