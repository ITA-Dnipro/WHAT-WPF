using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using Tetris.Model.Shape;

namespace Tetris.Model
{
    class GameManager
    {
        public const int COLUMNS = 10;
        public const int ROWS = 20;

        private int _score;
        private int _level;

        public FieldFiller Filler { get; } = new FieldFiller();
        public ShapeCreator FigureCreator { get; } = new ShapeCreator();
        public BaseShape MovingShape { get; set; }
        public BaseShape NextMovingShape { get; set; }

        public void Start(List<List<Rectangle>> _listOfRectangles, List<List<Rectangle>> _listOfNextRectangles, ref List<Coordinate> _previousShapeCoordinate)
        {
            _listOfRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#507387")); }));
            _listOfNextRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364c5c")); }));
            Filler.ListOfAllPoints.Clear();

            MovingShape = FigureCreator.CreateNewShape(GameManager.COLUMNS);

            _previousShapeCoordinate = CreateShape(MovingShape, _listOfRectangles, _previousShapeCoordinate);

            NextMovingShape = FigureCreator.CreateNewShape(4);
            _listOfNextRectangles = Filler.DrawShape(NextMovingShape, _listOfNextRectangles);

        }

        public void CreateNewShape(List<List<Rectangle>> _listOfRectangles, List<List<Rectangle>> _listOfNextRectangles, ref List<Coordinate> _previousShapeCoordinate)
        {
            _listOfNextRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364c5c")); }));

            MovingShape = NextMovingShape;

            _previousShapeCoordinate = CreateShape(MovingShape, _listOfRectangles, _previousShapeCoordinate);

            NextMovingShape = FigureCreator.CreateNewShape(4);
            _listOfNextRectangles = Filler.DrawShape(NextMovingShape, _listOfNextRectangles);

        }

        public List<Coordinate> CreateShape(BaseShape movingShape, List<List<Rectangle>> _listOfRectangles, List<Coordinate> _previousShapeCoordinate)
        {
            _listOfRectangles = Filler.DrawShape(MovingShape, _listOfRectangles);
            Filler.ListOfAllPoints.AddRange(MovingShape.Points);
            _previousShapeCoordinate = MovingShape.Points.ConvertAll(p => (Coordinate)p.Clone());

            return _previousShapeCoordinate;
        }

        public void CheckRowsForDeleting(List<List<Rectangle>> _listOfRectangles)
        {
            int delRows = 0;
            List<Coordinate> pointsToDel = new List<Coordinate>();
            for (int i = 0; i < GameManager.ROWS; i++)
            {
                pointsToDel = Filler.ListOfAllPoints.Where(p => p.X == i).ToList();
                if (pointsToDel.Count() >= GameManager.COLUMNS)
                {
                    pointsToDel.ForEach(p =>
                    {
                        _listOfRectangles[p.X][p.Y].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#507387"));
                        Filler.ListOfAllPoints.Remove(p);
                    });

                    int tempPos = 0;

                    for (int j = i; j > 0; j--)
                    {
                        for (int k = 0; k < COLUMNS; k++)
                        {
                            _listOfRectangles[j][k].Fill = _listOfRectangles[j - 1][k].Fill;
                            if (Filler.ListOfAllPoints.Exists(p => p.X == j && p.Y == k))
                            {
                                Filler.ListOfAllPoints.ForEach(p => { if (p.X == j && p.Y == k) p.X++; });
                            }
                        }
                    }

                    delRows++;
                }
            }
            if (delRows > 0)
            {
                switch (delRows)
                {
                    case 1:
                        _score += delRows * 40 + 40;
                        break;
                    case 2:
                        _score += delRows * 100 + 100;
                        break;
                    case 3:
                        _score += delRows * 300 + 300;
                        break;
                    case 4:
                        _score += delRows * 1200 + 1200;
                        break;
                }
                // if (_score >= ((_level + 1) * 3000) * 3 / 2) NewLevel();

            }
        }
    }
}
