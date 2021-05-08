using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Tetris.Model;
using Tetris.Model.Shape;

namespace Tetris.Views
{
    public partial class GameStartView : Page
    {
        public GameStartView()
        {
            InitializeComponent();
        }

        private GameManager _gameManager = new GameManager();
        private List<Coordinate> _previousShapeCoordinate = new List<Coordinate>();
        private Window _window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        //private BaseShape _nextMovingShape;
        //private BaseShape _movingShape;
        //private Random _rnd = new Random();
        private Rectangle _oneRectangle = new Rectangle();
        private List<List<Rectangle>> _listOfRectangles = new List<List<Rectangle>>(GameManager.COLUMNS);
        private List<List<Rectangle>> _listOfNextRectangles = new List<List<Rectangle>>(4);

        public void CreateMainGrid()
        {
           

            _oneRectangle = new Rectangle();

            _oneRectangle.Style = (Style)Application.Current.Resources["rectangleBlock"];//SetResourceReference(Control.StyleProperty, "rectangleBlock");

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
        _gameManager.Start(_listOfRectangles, _listOfNextRectangles, ref _previousShapeCoordinate);
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownMethod(e.Key);
        }

        private void KeyDownMethod(Key key)
        {
            if (_gameManager.MovingShape == null)
            {
                return;
            }

            //if (gm.IsEndOfGame)
            //{
            //    //AllDraw();
            //    return;
            //}
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
                _gameManager.CreateNewShape(_listOfRectangles, _listOfNextRectangles, ref _previousShapeCoordinate);
            }

            //AllDraw();
            //txtLevel.Text = gm.Level.ToString();
            //txtScore.Text = gm.Score.ToString();
            //  if (gm.IsEndOfGame) txtLabel.Text = "GAME OVER";
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            _window.Top -= 150;
            _window.Left -= 150;

            _window.Background = this.Background;
            Title = Assembly.GetExecutingAssembly().GetName().Name.ToString() + " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            _window.KeyDown += Page_KeyDown;
            _window.SizeChanged += Window_SizeChanged;

            CreateMainGrid();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = _window.Width;
            this.Height = _window.Height;
        }
    }
}
