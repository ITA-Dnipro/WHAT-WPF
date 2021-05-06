using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Tetris.Model.Shape
{
    abstract class BaseShape
    {
        private List<Coordinate> points = new List<Coordinate>();

        public List<Coordinate> Points { get { return points; } }

        public abstract void Create(int startX, int startY);

        public void SetColor(Color color)
        {
            points.ForEach(p => p.PointColor = color);
        }

        public bool CanMove(Key side, List<Coordinate> listOfAllShapesCoordinate)
        {
            bool moving = false;

            switch (side)
            {
                case Key.Down:
                    if (!points.Exists(p => (p.Y + 1) == GameManager.ROWS))
                    {
                        if (!listOfAllShapesCoordinate.Exists(allShapes => points.Exists(p => (p.Y + 1) == allShapes.X && p.Y == allShapes.Y)))
                        {
                            points.ForEach(p => { p.Y++; });
                            moving = true;
                        }
                    }
                    break;
                case Key.Left:
                    if (!points.Exists(p => (p.X - 1) < 0))
                    {
                        if (!listOfAllShapesCoordinate.Exists(allShapes => points.Exists(p => (p.X - 1) == allShapes.X && p.Y == allShapes.Y)))
                        {
                            points.ForEach(p => { p.X--; });
                            moving = true;
                        }
                    }
                    break;
                case Key.Right:
                    if (!points.Exists(p => (p.X + 1) == GameManager.COLUMNS))
                    {
                        if (!listOfAllShapesCoordinate.Exists(allShapes => points.Exists(p => (p.X + 1) == allShapes.X && p.Y == allShapes.Y)))
                        {
                            points.ForEach(p => { p.X++; });
                            moving = true;
                        }
                    }
                    break;
            }

            return moving;
        }
    }
}
