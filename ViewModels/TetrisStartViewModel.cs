using MsgBoxEx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Timers;
using System.Windows.Media;
using System.Windows.Shapes;

using Tetris.Models;
using Tetris.Models.Commands;
using Tetris.Models.Shape;

namespace Tetris.ViewModels
{
    class TetrisStartViewModel : INotifyPropertyChanged
    {
        public const int COLUMNS = 10;
        public const int ROWS = 20;
        public const int SIDE_COLUMNS = 4;
        public const int SIDE_ROWS = 2;
        public static readonly int[] scorePointsArray = { 40, 100, 300, 1200 };

        public event PropertyChangedEventHandler PropertyChanged;

        private int _score;
        private int _level = 1;
        private int _amountOfDeletedRows;
        private string _buttonPauseText = "Pause";
        private bool _isPaused;

        public FieldFiller Filler { get; set; } = new FieldFiller();
        public ShapeCreator FigureCreator { get; } = new ShapeCreator();
        public BaseShape MovingShape { get; set; }
        public BaseShape NextMovingShape { get; set; }

        private RelayCommand _buttonPause;
        private RelayCommand _buttonInfo;

        public bool IsEndOfGame { get; set; } = false;
        public int TimeOut { get; set; } = 1000;

        public int Score
        {
            get => _score;
            set
            {
                _score += value;
                OnPropertyChanged();
            }
        }

        public int Level 
        {
            get => _level;
            set
            {
                _level = value;
                OnPropertyChanged();
            }
        }

        public int AmountOfDeletedRows
        {
            get => _amountOfDeletedRows;
            set
            {
                _amountOfDeletedRows = value;
                OnPropertyChanged();
            }
        }

        public bool IsPaused
        {
            get => _isPaused;
            set
            {
                _isPaused = value;
                OnPropertyChanged();
            }
        }

        public string ButtonPauseText
        {
            get => _buttonPauseText;
            set
            {
                _buttonPauseText = value;
                OnPropertyChanged();
            }
        }

        public void SetGameOnPause()
        {
            if (MovingShape == null)
            {
                return;
            }

            if (IsPaused)
            {
                ButtonPauseText = "Pause";
                IsPaused = false;
            }
            else
            {
                ButtonPauseText = "Resume";
                IsPaused = true;
            }
        }

        public void Start(List<List<Rectangle>> _listOfRectangles, List<List<Rectangle>> _listOfNextRectangles, ref List<Coordinate> _previousShapeCoordinate)
        {
            _listOfRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#507387")); }));
            _listOfNextRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364c5c")); }));

            MovingShape = FigureCreator.CreateNewShape(TetrisStartViewModel.COLUMNS);

            _previousShapeCoordinate = CreateShape(_listOfRectangles, _previousShapeCoordinate);

            NextMovingShape = FigureCreator.CreateNewShape(TetrisStartViewModel.SIDE_COLUMNS);
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

            for (int i = 0; i < TetrisStartViewModel.ROWS; i++)
            {
                pointsToDel = Filler.ListOfAllPoints.Where(p => p.X == i).ToList();
                if (pointsToDel.Count() >= TetrisStartViewModel.COLUMNS)
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

        public void RemoveLines(int delRows, int firstFoundedLine, List<List<Rectangle>> _listOfRectangles)
        {
            for (int i = 0; i < delRows; i++)
            {
                _listOfRectangles[firstFoundedLine + i].ForEach(p =>
                {
                    p.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#507387"));
                });

                Filler.ListOfAllPoints.Where(p => p.X == i);

                Filler.ListOfAllPoints.RemoveAll(point => point.X == firstFoundedLine + i);

                ShiftToDown(firstFoundedLine + i, _listOfRectangles);
            }
        }

        private void ShiftToDown(int firstBottomLine, List<List<Rectangle>> _listOfRectangles)
        {
            for (int j = firstBottomLine; j > 0; j--)
            {
                for (int k = 0; k < TetrisStartViewModel.COLUMNS; k++)
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

        public RelayCommand ButtonPause
        {
            get
            {
                return _buttonPause ?? (_buttonPause = new RelayCommand(parameter =>
                {
                    SetGameOnPause();
                }));
            }
        }

        public RelayCommand ButtonInfo
        {
            get
            {
                return _buttonInfo ?? (_buttonInfo = new RelayCommand(parameter =>
                {
                    if (IsPaused == false && MovingShape != null)
                    {
                        SetGameOnPause();
                    }
                    MessageBoxEx.Show("← Move left \n→ Move right \n↓ Move down \n↑ Rotate\n\n F11 - Fullscreen\n Esc - Exit to main menu", "Info", this);
                }));
            }
        }

        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
