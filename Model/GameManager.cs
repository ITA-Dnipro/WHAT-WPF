using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Media;
using System.Windows.Shapes;

using Tetris.Model.Shape;

namespace Tetris.Model
{
    class GameManager
    {
        public const int COLUMNS = 10;
        public const int ROWS = 20;
        public static readonly int[] scorePointsArray = { 40, 100, 300, 1200 }; 

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

        public int AmountOfDeletedRows { get; set; } = 0;

        public bool IsPaused { get; set; }


        public void Start(List<List<Rectangle>> _listOfRectangles, List<List<Rectangle>> _listOfNextRectangles, ref List<Coordinate> _previousShapeCoordinate)
        {
            MovingThread = new Thread(MoveDownByThread);
            MovingThread.IsBackground = false;
            MovingThread.Start();

            _listOfRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#507387")); }));
            _listOfNextRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364c5c")); }));

            MovingShape = FigureCreator.CreateNewShape(GameManager.COLUMNS);

            _previousShapeCoordinate = CreateShape(_listOfRectangles, _previousShapeCoordinate);

            NextMovingShape = FigureCreator.CreateNewShape(4);
            _listOfNextRectangles = Filler.DrawShape(NextMovingShape, _listOfNextRectangles);

        }

        public bool CreateNextShape(List<List<Rectangle>> _listOfRectangles, List<List<Rectangle>> _listOfNextRectangles, ref List<Coordinate> _previousShapeCoordinate)
        {
            _listOfNextRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364c5c")); }));

            NextMovingShape.Points.ForEach(point => point.Y += 3);

            MovingShape = NextMovingShape;

            _previousShapeCoordinate = CreateShape(_listOfRectangles, _previousShapeCoordinate);

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

        public List<Coordinate> CreateShape(List<List<Rectangle>> _listOfRectangles, List<Coordinate> _previousShapeCoordinate)
        {
            _listOfRectangles = Filler.DrawShape(MovingShape, _listOfRectangles);
            Filler.ListOfAllPoints.AddRange(MovingShape.Points);
            _previousShapeCoordinate = MovingShape.Points.ConvertAll(p => (Coordinate)p.Clone());

            return _previousShapeCoordinate;
        }

        public int CheckRowsForDeleting(ref int lastFoundedLine)
        {
            int delRows = 0;
            List<Coordinate> pointsToDel = new List<Coordinate>();

            for (int i = 0; i < GameManager.ROWS; i++)
            {
                pointsToDel = Filler.ListOfAllPoints.Where(p => p.X == i).ToList();
                if (pointsToDel.Count() >= GameManager.COLUMNS)
                {
                    delRows++;
                    lastFoundedLine = i;

                    if (delRows == 4)
                    {
                        break;
                    }
                }
            }

            AmountOfDeletedRows += delRows;

            return delRows;
        }


        public void ShiftToDown(int firstBottomLine, List<List<Rectangle>> _listOfRectangles)
        {
            for (int j = firstBottomLine; j > 0; j--)
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
        }

        public void UpdateScore(int delRows)
        {
            Score += delRows * scorePointsArray[delRows - 1] + scorePointsArray[delRows - 1];
        }

        public void LevelUp()
        {
            if (AmountOfDeletedRows >= Level * 10)
            {
                Level++;
                TimeOut = (int)(TimeOut * 0.9f);
            }
        }

        private void MoveDownByThread()
        {
            while (!IsEndOfGame )
            {
                if (MoveDownByThr != null && IsPressed == false)
                {
                    MoveDownByThr();
                }

                IsPressed = false;

                Thread.Sleep(TimeOut);
            }

            MovingThread.Abort();
        }
    }
}
