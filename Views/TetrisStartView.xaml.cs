using MsgBoxEx;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

using Tetris.Model;

namespace Tetris.Views
{
    public partial class TetrisStartView : Window
    {
        public TetrisStartView()
        {
            InitializeComponent();
            this.InitMessageBox();
        }

        private void InitMessageBox()
        {
            // font family name is validate against fonts installed in windows.
            MessageBoxEx.SetFont("Verdana", 14);

            MessageBoxEx.SetMessageForeground(Colors.White);
            MessageBoxEx.SetMessageBackground(Colors.Black);
            MessageBoxEx.SetButtonBackground(MessageBoxEx.ColorFromString("#26283b"));

            // template name is validated and if not found in your app, will not be applied
            MessageBoxEx.SetButtonTemplateName("AefCustomButton");

            // default max width is the width of the primary screen's work area minus 100 pixels
            MessageBoxEx.SetMaxFormWidth(1000);
        }

        private GameManager _gameManager = new GameManager();
        private List<Coordinate> _previousShapeCoordinate = new List<Coordinate>();
        private Rectangle _oneRectangle = new Rectangle();
        private List<List<Rectangle>> _listOfRectangles = new List<List<Rectangle>>(GameManager.COLUMNS);
        private List<List<Rectangle>> _listOfNextRectangles = new List<List<Rectangle>>(4);
        private bool firstCheck = true;

        public void CreateMainGrid()
        {
            Style rectangleStyle = this.FindResource("rectangleBlock") as Style;
            Style nextRectangleStyle = this.FindResource("nextRectangleBlock") as Style;

            _listOfRectangles = CreateGrid(GameManager.ROWS, GameManager.COLUMNS, _listOfRectangles, rectangleStyle, mainGrid);
            _listOfNextRectangles = CreateGrid(2, 4, _listOfNextRectangles, nextRectangleStyle, sideGrid);
        }

        public List<List<Rectangle>> CreateGrid(int rows, int cols, List<List<Rectangle>> _listOfRectangles, Style style, Grid gridName)
        {
            for (int j = 0; j < rows; j++)
            {
                _listOfRectangles.Add(new List<Rectangle>());

                for (int i = 0; i < cols; i++)
                {
                    _oneRectangle = new Rectangle
                    {
                        Style = style
                    };

                    Grid.SetColumn(_oneRectangle, i);
                    Grid.SetRow(_oneRectangle, j);
                    gridName.Children.Add(_oneRectangle);

                    _listOfRectangles[j].Add(_oneRectangle);
                }
            }

            return _listOfRectangles;
        }

        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            score.Text = "0";
            level.Text = "1";
            deletedRows.Text = "0";
            buttonStart.Focusable = false;
            

            if (_gameManager.MovingThread != null)
            {
                _gameManager.MovingThread.Abort();
            }

            bool isPaused = _gameManager.IsPaused;

            _gameManager = new GameManager();

            if (isPaused)
            {
                _gameManager.IsPaused = true;
            }

            _gameManager.Start(_listOfRectangles, _listOfNextRectangles, ref _previousShapeCoordinate);

            _gameManager.MoveDownByThr += MoveDownByThread;
        }

        private void Button_Pause_Click(object sender, RoutedEventArgs e)
        {
            SetGameOnPause();
        }

        private void SetGameOnPause()
        {
            if (_gameManager.MovingShape == null)
            {
                return;
            }

            if (_gameManager.IsPaused)
            {
                buttonPause.Content = "Pause";
                _gameManager.IsPaused = false;
            }
            else
            {
                buttonPause.Content = "Reset";
                _gameManager.IsPaused = true;
            }
        }

        private void Button_Info_Click(object sender, RoutedEventArgs e)
        {
            _gameManager.IsPaused = true;
            MessageBoxEx.Show("← Move left \n→ Move right \n↓ Move down \n↑ Rotate\n\n F11 - Fullscreen\n Esc - Exit to main menu", "Info");
            _gameManager.IsPaused = false;
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F11:
                    if (this.WindowState == WindowState.Normal)
                    {
                        this.WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        this.WindowState = WindowState.Normal;
                    }
                    break;
                case Key.Escape:

                    if (_gameManager.IsPaused == false)
                    {
                        SetGameOnPause();
                    }

                    MessageBoxResult exitResult = MessageBoxEx.Show("Do you really want exit to main menu?", "Exit", MessageBoxButton.YesNo);

                    if (exitResult == MessageBoxResult.Yes)
                    {
                        MenuView menu = new MenuView();
                        menu.Show();
                        this.Close();
                    }

                    break;
                case Key.Up: 
                case Key.Down: 
                case Key.Left: 
                case Key.Right:
                    KeyDownMethod(e.Key);
                    break;
            }
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
            if (_gameManager.MovingShape == null || _gameManager.IsEndOfGame || _gameManager.IsPaused)
            {
                return;
            }

            _listOfRectangles = _gameManager.Filler.ClearPreviousShape(_previousShapeCoordinate, _listOfRectangles);

            MoveShape(key);

            bool isCollision = false;

            if (_gameManager.MovingShape.Points.Exists(p => _gameManager.Filler.ListOfAllPoints.Exists(point => point.X == p.X + 1 && p.Y == point.Y)))
            {
                isCollision = true;
            }

            _gameManager.Filler.ListOfAllPoints.AddRange(_gameManager.MovingShape.Points);
            _listOfRectangles = _gameManager.Filler.DrawShape(_gameManager.MovingShape, _listOfRectangles);
            _previousShapeCoordinate = _gameManager.MovingShape.Points.ConvertAll(p => (Coordinate)p.Clone());

            CheckCollisionLastRow(isCollision);
        }

        public void MoveShape(Key key)
        {
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
                    if (_gameManager.MovingShape.CanRotate(_gameManager.Filler.ListOfAllPoints) && !_gameManager.IsPaused)
                    {
                        _gameManager.MovingShape.Rotate();
                    }
                    break;
            }
        }

        public void CheckCollisionLastRow( bool isCollision)
        {
            if (_gameManager.MovingShape.Points.Exists(p => _gameManager.Filler.ListOfAllPoints.Exists(point => (p.X + 1) == GameManager.ROWS || isCollision == true)))
            {
                if (!firstCheck)
                {
                    score.Text = _gameManager.CheckRowsForDeleting(_listOfRectangles).ToString();
                    level.Text = (_gameManager.Level).ToString();
                    deletedRows.Text = _gameManager.AmountOfDeletedRows.ToString();

                    if (_gameManager.CreateNextShape(_listOfRectangles, _listOfNextRectangles, ref _previousShapeCoordinate))
                    {
                        _listOfRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#507387")); }));
                        _listOfNextRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364c5c")); }));

                        MessageBoxEx.SetMessageForeground(Colors.Red);
                        MessageBoxEx.Show("GAME OVER", "Game Over");
                        MessageBoxEx.SetMessageForeground(Colors.White);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
