using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

namespace Tetris.Model.Shape
{
    abstract class BaseShape
    {
        public IMovements movements = new ShapeMovements();

        private readonly List<Coordinate> points = new List<Coordinate>();

        public List<Coordinate> Points { get { return points; } }

        public abstract void Create(int startX, int startY);

        public void SetColor(LinearGradientBrush color)
        {
            points.ForEach(p => p.PointColor = color);
        }
    }  
}
