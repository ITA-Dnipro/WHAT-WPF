using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public int Score { get; set; }
        public int Level { get; set; } = 1;

        public FieldFiller Filler { get; set; } = new FieldFiller();
        public ShapeCreator FigureCreator { get; } = new ShapeCreator();
        public BaseShape MovingShape { get; set; }
        public BaseShape NextMovingShape { get; set; }
        public delegate void MoveDownByThreadHandler();
        public event MoveDownByThreadHandler MoveDownByThr;

        public bool IsEndOfGame { get; set; } = false;
        public bool IsPressed { get; set; } = false;
        public Thread MovingThread { get; set; }
        public int TimeOut { get; set; } = 1000;


        public void Start(List<List<Rectangle>> _listOfRectangles, List<List<Rectangle>> _listOfNextRectangles, ref List<Coordinate> _previousShapeCoordinate)
        {
            MovingThread = new Thread(MoveDownByThread);
            MovingThread.IsBackground = false;
            MovingThread.Start();

            _listOfRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#507387")); }));
            _listOfNextRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364c5c")); }));

            MovingShape = FigureCreator.CreateNewShape(GameManager.COLUMNS);

            _previousShapeCoordinate = CreateShape(MovingShape, _listOfRectangles, _previousShapeCoordinate);

            NextMovingShape = FigureCreator.CreateNewShape(4);
            _listOfNextRectangles = Filler.DrawShape(NextMovingShape, _listOfNextRectangles);

        }

        public bool CreateNextShape(List<List<Rectangle>> _listOfRectangles, List<List<Rectangle>> _listOfNextRectangles, ref List<Coordinate> _previousShapeCoordinate)
        {
            _listOfNextRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364c5c")); }));

            MovingShape = NextMovingShape;

            _previousShapeCoordinate = CreateShape(MovingShape, _listOfRectangles, _previousShapeCoordinate);

            NextMovingShape = FigureCreator.CreateNewShape(4);
            _listOfNextRectangles = Filler.DrawShape(NextMovingShape, _listOfNextRectangles);


            _listOfRectangles = Filler.ClearPreviousShape(_previousShapeCoordinate, _listOfRectangles);

            if (MovingShape.Points.Exists(p => Filler.ListOfAllPoints.Exists(point => point.X == p.X + 1 && p.Y == point.Y && p.X == 0)))
            {
                IsEndOfGame = true;
            }
            else
            {
                _listOfRectangles = Filler.DrawShape(MovingShape, _listOfRectangles);
                Filler.ListOfAllPoints.AddRange(MovingShape.Points);
            }

            return IsEndOfGame;
        }

        public List<Coordinate> CreateShape(BaseShape movingShape, List<List<Rectangle>> _listOfRectangles, List<Coordinate> _previousShapeCoordinate)
        {
            _listOfRectangles = Filler.DrawShape(MovingShape, _listOfRectangles);
            Filler.ListOfAllPoints.AddRange(MovingShape.Points);
            _previousShapeCoordinate = MovingShape.Points.ConvertAll(p => (Coordinate)p.Clone());

            return _previousShapeCoordinate;
        }

        public int CheckRowsForDeleting(List<List<Rectangle>> _listOfRectangles)
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
                        Score += delRows * 40 + 40;
                        break;
                    case 2:
                        Score += delRows * 100 + 100;
                        break;
                    case 3:
                        Score += delRows * 300 + 300;
                        break;
                    case 4:
                        Score += delRows * 1200 + 1200;
                        break;
                }
            }

            if (Score >= ((Level + 1) * 3000) * 3 / 2)
            {
                LevelUp();
            }

            return Score;
        }

        public void LevelUp()
        {
            Level++;
          TimeOut = (int)(TimeOut * 0.8f);
        }

        private void MoveDownByThread()
        {
            while (!IsEndOfGame )
            {
                if (IsEndOfGame) MovingThread.Abort();

                if (MoveDownByThr != null && IsPressed == false)
                {
                    MoveDownByThr();
                }

                IsPressed = false;

                Thread.Sleep(TimeOut);

            }
        }
    }
}
