using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Tetris.Model;

namespace Tetris.Views
{
    public partial class TetrisStartView : Window
    {
        public TetrisStartView()
        {
            InitializeComponent();
        }

        private GameManager _gameManager = new GameManager();
        private List<Coordinate> _previousShapeCoordinate = new List<Coordinate>();
        private Rectangle _oneRectangle = new Rectangle();
        private List<List<Rectangle>> _listOfRectangles = new List<List<Rectangle>>(GameManager.COLUMNS);
        private List<List<Rectangle>> _listOfNextRectangles = new List<List<Rectangle>>(4);
        private bool firstCheck = true;

        public void CreateMainGrid()
        {
            for (int j = 0; j < GameManager.ROWS; j++)
            {
                _listOfRectangles.Add(new List<Rectangle>());

                for (int i = 0; i < GameManager.COLUMNS; i++)
                {
                    _oneRectangle = new Rectangle
                    {
                        Stretch = Stretch.Fill,
                        Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#507387")),
                        Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#363835"))
                    };

                    Grid.SetColumn(_oneRectangle, i);
                    Grid.SetRow(_oneRectangle, j);
                    mainGrid.Children.Add(_oneRectangle);

                    _listOfRectangles[j].Add(_oneRectangle);
                }
            }

            for (int j = 0; j < 4; j++)
            {
                _listOfNextRectangles.Add(new List<Rectangle>());

                for (int i = 0; i < 4; i++)
                {
                    _oneRectangle = new Rectangle
                    {
                        Stretch = Stretch.Fill,
                        Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364c5c")),
                        Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364c5c"))
                    };

                    Grid.SetColumn(_oneRectangle, i);
                    Grid.SetRow(_oneRectangle, j);
                    sideGrid.Children.Add(_oneRectangle);

                    _listOfNextRectangles[j].Add(_oneRectangle);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            score.Text = "0";
            level.Text = "1";
            gameOver.Text = "";

            if (_gameManager.MovingThread != null)
            {
                _gameManager.MovingThread.Abort();
            }

            _gameManager = new GameManager();
            _gameManager.Start(_listOfRectangles, _listOfNextRectangles, ref _previousShapeCoordinate);

            _gameManager.MoveDownByThr += MoveDownByThread;
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownMethod(e.Key);
        }

        void MoveDownByThread()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                KeyDownMethod(Key.Down);
            }));
        }

        private void KeyDownMethod(Key key)
        {
            if (_gameManager.MovingShape == null)
            {
                return;
            }

            if (_gameManager.IsEndOfGame)
            {
                return;
            }

            _listOfRectangles = _gameManager.Filler.ClearPreviousShape(_previousShapeCoordinate, _listOfRectangles);

            switch (key)
            {
                case Key.Left:
                    if (_gameManager.MovingShape.CanMove(Key.Left, _gameManager.Filler.ListOfAllPoints))
                    {
                        _gameManager.MovingShape.Move(Key.Left);
                    }
                    break;
                case Key.Right:
                    if (_gameManager.MovingShape.CanMove(Key.Right, _gameManager.Filler.ListOfAllPoints))
                    {
                        _gameManager.MovingShape.Move(Key.Right);
                    }
                    break;
                case Key.Down:
                    if (_gameManager.MovingShape.CanMove(Key.Down, _gameManager.Filler.ListOfAllPoints))
                    {
                        _gameManager.MovingShape.Move(Key.Down);
                        _gameManager.IsPressed = true;
                    }
                    break;
                case Key.Up:
                    if (_gameManager.MovingShape.CanRotate(_gameManager.Filler.ListOfAllPoints))
                    {
                        _gameManager.MovingShape.Rotate();
                    }
                    break;
            }

            bool isCollision = false;

            if (_gameManager.MovingShape.Points.Exists(p => _gameManager.Filler.ListOfAllPoints.Exists(point => point.X == p.X + 1 && p.Y == point.Y)))
            {
                isCollision = true;
            }

            _gameManager.Filler.ListOfAllPoints.AddRange(_gameManager.MovingShape.Points);
            _listOfRectangles = _gameManager.Filler.DrawShape(_gameManager.MovingShape, _listOfRectangles);
            _previousShapeCoordinate = _gameManager.MovingShape.Points.ConvertAll(p => (Coordinate)p.Clone());

            if (_gameManager.MovingShape.Points.Exists(p => _gameManager.Filler.ListOfAllPoints.Exists(point => (p.X + 1) == GameManager.ROWS || isCollision == true)))
            {
                if (!firstCheck)
                {
                    score.Text = _gameManager.CheckRowsForDeleting(_listOfRectangles).ToString();
                    level.Text = (_gameManager.Level + 1).ToString();

                    if (_gameManager.CreateNextShape(_listOfRectangles, _listOfNextRectangles, ref _previousShapeCoordinate))
                    {
                        _listOfRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#507387")); }));
                        _listOfNextRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364c5c")); }));
                        gameOver.Text = "GAME OVER";
                        return;
                    }

                    firstCheck = true;
                }
                else
                {
                    firstCheck = false;
                }

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateMainGrid();   
        }
    }
}
