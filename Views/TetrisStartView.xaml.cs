using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

using MsgBoxEx;
using Tetris.Model;

namespace Tetris.Views
{
    public partial class TetrisStartView : Window
    {
        public TetrisStartView()
        {
            InitializeComponent();
            InitMessageBox();
        }

        private void InitMessageBox()
        {
            // font family name is validate against fonts installed in windows.
            MessageBoxEx.SetFont("Verdana", 14);

            MessageBoxEx.SetMessageForeground(Colors.White);
            MessageBoxEx.SetMessageBackground(Colors.Black);
            MessageBoxEx.SetButtonBackground(MessageBoxEx.ColorFromString("#26283b"));

            // default max width is the width of the primary screen's work area minus 100 pixels
            MessageBoxEx.SetMaxFormWidth(1000);
        }

        private GameManager _gameManager = new GameManager();
        private List<Coordinate> _previousShapeCoordinate = new List<Coordinate>();
        private Rectangle _oneRectangle = new Rectangle();
        private List<List<Rectangle>> _listOfRectangles = new List<List<Rectangle>>(GameManager.COLUMNS);
        private List<List<Rectangle>> _listOfNextRectangles = new List<List<Rectangle>>(4);
        private bool firstCheck = true;
        private Timer timer;

        public void CreateMainGrid()
        {
            Style rectangleStyle = this.FindResource("rectangleBlock") as Style;
            Style nextRectangleStyle = this.FindResource("nextRectangleBlock") as Style;

            _listOfRectangles = CreateGrid(GameManager.ROWS, GameManager.COLUMNS, _listOfRectangles, rectangleStyle, mainGrid);
            _listOfNextRectangles = CreateGrid(GameManager.SIDE_ROWS, GameManager.SIDE_COLUMNS, _listOfNextRectangles, nextRectangleStyle, sideGrid);
        }

        public List<List<Rectangle>> CreateGrid(int rows, int cols, List<List<Rectangle>> _listOfRectangles, Style style, Grid gridName)
        {
            for (int i = 0; i < rows; i++)
            {
                RowDefinition row = new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Star)
                };

                gridName.RowDefinitions.Add(row);
            }

            for (int i = 0; i < cols; i++)
            {
                ColumnDefinition column = new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                };

                gridName.ColumnDefinitions.Add(column);
            }

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
            textBoxNext.Visibility = Visibility.Visible;

            bool isPaused = _gameManager.IsPaused;

            _gameManager = new GameManager();

            if (isPaused)
            {
                _gameManager.IsPaused = true;
            }

            if (!timer.Enabled)
            {
                timer.Start();
            }

            _gameManager.Start(_listOfRectangles, _listOfNextRectangles, ref _previousShapeCoordinate);
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
                buttonPause.Content = "Resume";
                _gameManager.IsPaused = true;
            }
        }

        private void Button_Info_Click(object sender, RoutedEventArgs e)
        {
            if (_gameManager.IsPaused == false)
            {
                SetGameOnPause();
            }
            MessageBoxEx.Show("← Move left \n→ Move right \n↓ Move down \n↑ Rotate\n\n F11 - Fullscreen\n Esc - Exit to main menu", "Info", this);
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

                    MessageBoxResult exitResult = MessageBoxEx.Show("Do you really want exit to main menu?", "Exit", MessageBoxButton.YesNo, this);

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

        void MoveDownByThread(Object source, ElapsedEventArgs e)
        {
            if (_gameManager.IsEndOfGame)
            {
                timer.Stop();
            }
            else
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    KeyDownMethod(Key.Down);
                }));
            }
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
                    if (_gameManager.MovingShape.movements.CanMove(Key.Left, _gameManager.Filler.ListOfAllPoints, _gameManager.MovingShape.Points))
                    {
                        _gameManager.MovingShape.movements.Move(Key.Left, _gameManager.MovingShape.Points);
                    }
                    break;
                case Key.Right:
                    if (_gameManager.MovingShape.movements.CanMove(Key.Right, _gameManager.Filler.ListOfAllPoints, _gameManager.MovingShape.Points))
                    {
                        _gameManager.MovingShape.movements.Move(Key.Right, _gameManager.MovingShape.Points);
                    }
                    break;
                case Key.Down:
                    if (_gameManager.MovingShape.movements.CanMove(Key.Down, _gameManager.Filler.ListOfAllPoints, _gameManager.MovingShape.Points))
                    {
                        timer.Stop();
                        _gameManager.MovingShape.movements.Move(Key.Down, _gameManager.MovingShape.Points);
                        timer.Start();
                    }
                    break;
                case Key.Up:
                    if (_gameManager.MovingShape.movements.CanRotate(_gameManager.Filler.ListOfAllPoints, _gameManager.MovingShape.Points) && !_gameManager.IsPaused)
                    {
                        _gameManager.MovingShape.movements.Rotate(_gameManager.MovingShape.Points);
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
                    int lastFoundedLine = 0;
                    int delRows = _gameManager.CheckRowsForDeleting(ref lastFoundedLine);

                    if  (delRows > 0)
                    {

                        int firstFoundedLine = lastFoundedLine - delRows + 1;

                        RemoveLines(delRows, firstFoundedLine);
                        _gameManager.UpdateScore(delRows);
                        _gameManager.LevelUp();
                        timer.Interval = _gameManager.TimeOut;
                    }

                    score.Text = _gameManager.Score.ToString();
                    level.Text = (_gameManager.Level).ToString();
                    deletedRows.Text = _gameManager.AmountOfDeletedRows.ToString();

                    if (_gameManager.CreateNextShape(_listOfRectangles, _listOfNextRectangles, ref _previousShapeCoordinate))
                    {
                        _listOfRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#507387")); }));
                        _listOfNextRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364c5c")); }));

                        textBoxNext.Visibility = Visibility.Collapsed;
                        overlay.Visibility = Visibility.Visible;
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

        private void RemoveLines(int delRows,int firstFoundedLine)
        {
            for (int i = 0; i < delRows; i++)
            {
                _listOfRectangles[firstFoundedLine + i].ForEach(p =>
                {
                    p.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#507387"));
                });

                _gameManager.Filler.ListOfAllPoints.Where(p => p.X == i);

                _gameManager.Filler.ListOfAllPoints.RemoveAll(point => point.X == firstFoundedLine + i);

                ShiftToDown(firstFoundedLine + i);
            }
        }

        public void ShiftToDown(int firstBottomLine)
        {
            for (int j = firstBottomLine; j > 0; j--)
            {
                for (int k = 0; k < GameManager.COLUMNS; k++)
                {
                    _listOfRectangles[j][k].Fill = _listOfRectangles[j - 1][k].Fill;
                    if (_gameManager.Filler.ListOfAllPoints.Exists(p => p.X == j && p.Y == k))
                    {
                        _gameManager.Filler.ListOfAllPoints.ForEach(p => { if (p.X == j && p.Y == k) p.X++; });
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateMainGrid();

            InitializeTimer();
        }

       private void InitializeTimer()
        {
            timer = new Timer
            {
                Interval = _gameManager.TimeOut,
            };

            timer.Elapsed += MoveDownByThread;
        }

        private void Button__EndGame_Click(object sender, RoutedEventArgs e)
        {
            overlay.Visibility = Visibility.Collapsed;
            score.Text = "0";
            level.Text = "1";
            deletedRows.Text = "0";
        }
    }
}
