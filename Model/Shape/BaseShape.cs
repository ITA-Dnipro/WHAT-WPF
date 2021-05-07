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
                    if (!points.Exists(p => (p.X + 1) == GameManager.ROWS))
                    {
                        if (listOfAllShapesCoordinate.Exists(shapeCoordinate => points.Exists(p => (p.X + 1) != shapeCoordinate.X && p.Y == shapeCoordinate.Y)))
                        {
                            moving = true;
                        }
                    }
                    break;
                case Key.Left:
                    if (!points.Exists(p => (p.Y - 1) < 0))
                    {
                        if (listOfAllShapesCoordinate.Exists(allShapes => points.Exists(p => (p.Y - 1) != allShapes.Y && p.X == allShapes.X)))
                        {
                            moving = true;
                        }
                    }
                    break;
                case Key.Right:
                    if (!points.Exists(p => (p.Y + 1) == GameManager.COLUMNS))
                    {
                        if (listOfAllShapesCoordinate.Exists(allShapes => points.Exists(p => (p.Y + 1) != allShapes.Y && p.X == allShapes.X)))
                        {
                            moving = true;
                        }
                    }
                    break;
            }

            return moving;
        }

        public void Move(Key side)
        {
            switch (side)
            {
                case Key.Down:
                    points.ForEach(p => { p.X++; });
                    break;
                case Key.Left:
                    points.ForEach(p => { p.Y--; });
                    break;
                case Key.Right:
                    points.ForEach(p => { p.Y++; });
                    break;
            }
        }

        public void Rotate()
        {
            List<Coordinate> testPoints = new List<Coordinate>(points.Count());

            foreach (var point in points)
            {
                int temp = point.X;
                point.X = point.Y;
                point.Y = temp;
            }

            int xMax = (points.Max(p => p.X));
            int yMax = (points.Max(p => p.Y));
            int xMin = (points.Min(p => p.X));
            int yMin = (points.Min(p => p.Y));

            int xLength = xMax - xMin + 1;
            int yLength = yMax - yMin + 1;

            if (xLength > yLength) xMin++;
            if (yLength > xLength) xMin--;

            foreach (var point in points)
            {
                testPoints.Add(new Coordinate(Math.Abs(point.Y - yMax) + xMin, (point.X - xMin) + yMin, point.PointColor));
            }

            points.Clear();
            points.AddRange(testPoints);

            foreach (var point in points)
            {
                int temp = point.X;
                point.X = point.Y;
                point.Y = temp;
            }
        }
    }
}
