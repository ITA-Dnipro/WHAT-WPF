using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tetris.Model.Shape
{
    class ShapeMovements : IMovements
    {
        public bool CanMove(Key side, List<Coordinate> listOfAllShapesCoordinate, List<Coordinate> points)
        {
            bool moving = false;

            switch (side)
            {
                case Key.Down:
                    if (!points.Exists(p => (p.X + 1) == GameManager.ROWS))
                    {
                        if (!listOfAllShapesCoordinate.Exists(shapeCoordinate => points.Exists(p => (p.X + 1) == shapeCoordinate.X && p.Y == shapeCoordinate.Y)))
                        {
                            moving = true;
                        }
                    }
                    break;
                case Key.Left:
                    if (!points.Exists(p => (p.Y - 1) < 0))
                    {
                        if (!listOfAllShapesCoordinate.Exists(shapeCoordinate => points.Exists(p => (p.Y - 1) == shapeCoordinate.Y && p.X == shapeCoordinate.X)))
                        {
                            moving = true;
                        }
                    }
                    break;
                case Key.Right:
                    if (!points.Exists(p => (p.Y + 1) == GameManager.COLUMNS))
                    {
                        if (!listOfAllShapesCoordinate.Exists(shapeCoordinate => points.Exists(p => (p.Y + 1) == shapeCoordinate.Y && p.X == shapeCoordinate.X)))
                        {
                            moving = true;
                        }
                    }
                    break;
            }

            return moving;
        }

        public void Move(Key side, List<Coordinate> points)
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

        public bool CanRotate(List<Coordinate> listOfAllShapesCoordinate, List<Coordinate> points)
        {
            bool rotating = false;

            Swap(points);

            List<Coordinate> testPoints = RotatePreparation(points);

            Swap(testPoints);


            if (!testPoints.Exists(p => p.X >= GameManager.ROWS || p.X < 0 || p.Y < 0 || p.Y >= GameManager.COLUMNS))
            {
                if (!listOfAllShapesCoordinate.Exists(coord => testPoints.Exists(p => p.X == coord.X && p.Y == coord.Y)))
                {
                    rotating = true;
                }
            }

            Swap(points);

            return rotating;
        }

        private List<Coordinate> RotatePreparation(List<Coordinate> points)
        {
            List<Coordinate> testPoints = new List<Coordinate>(points.Count());

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

            return testPoints;
        }

        private void Swap(List<Coordinate> points)
        {
            foreach (var point in points)
            {
                int temp = point.X;
                point.X = point.Y;
                point.Y = temp;
            }
        }

        public void Rotate(List<Coordinate> points)
        {
            Swap(points);

            List<Coordinate> testPoints = RotatePreparation(points);

            points.Clear();
            points.AddRange(testPoints);

            Swap(points);
        }
    }
}

