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
using Tetris.Models;
using Tetris.ViewModels;

namespace Tetris.Views
{
    public partial class TetrisStartView : Window
    {
        public TetrisStartView()
        {
            InitializeComponent();
            InitializeFields();
            InitializeDataContext();
            InitializeMessageBox();
        }

        private TetrisStartViewModel _startViewModel;
        private List<Coordinate> _previousShapeCoordinate;
        private Rectangle _oneRectangle;
        private List<List<Rectangle>> _listOfRectangles;
        private List<List<Rectangle>> _listOfNextRectangles;
        private bool firstCheck = true;
        private Timer timer;

        private void InitializeFields()
        {
            _startViewModel = new TetrisStartViewModel();
            _previousShapeCoordinate = new List<Coordinate>();
            _oneRectangle = new Rectangle();
            _listOfRectangles = new List<List<Rectangle>>(TetrisStartViewModel.COLUMNS);
            _listOfNextRectangles = new List<List<Rectangle>>(TetrisStartViewModel.SIDE_COLUMNS);
        }

        private void InitializeDataContext()
        {
            DataContext = _startViewModel;
        }

        private void InitializeMessageBox()
        {
            // font family name is validate against fonts installed in windows.
            MessageBoxEx.SetFont("Verdana", 14);

            MessageBoxEx.SetMessageForeground(Colors.White);
            MessageBoxEx.SetMessageBackground(Colors.Black);
            MessageBoxEx.SetButtonBackground(MessageBoxEx.ColorFromString("#26283b"));

            // default max width is the width of the primary screen's work area minus 100 pixels
            MessageBoxEx.SetMaxFormWidth(1000);
        }

        private void CreateMainGrid()
        {
            Style rectangleStyle = this.FindResource("rectangleBlock") as Style;
            Style nextRectangleStyle = this.FindResource("nextRectangleBlock") as Style;

            _listOfRectangles = CreateGrid(TetrisStartViewModel.ROWS, TetrisStartViewModel.COLUMNS, _listOfRectangles, rectangleStyle, mainGrid);
            _listOfNextRectangles = CreateGrid(TetrisStartViewModel.SIDE_ROWS, TetrisStartViewModel.SIDE_COLUMNS, _listOfNextRectangles, nextRectangleStyle, sideGrid);
        }

        private Grid CreateRowsDefinition(int rows, Grid gridName)
        {
            for (int i = 0; i < rows; i++)
            {
                RowDefinition row = new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Star)
                };

                gridName.RowDefinitions.Add(row);
            }

            return gridName;
        }

        private Grid CreateColumnsDefinition(int cols, Grid gridName)
        {
            for (int i = 0; i < cols; i++)
            {
                ColumnDefinition column = new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                };

                gridName.ColumnDefinitions.Add(column);
            }

            return gridName;
        }

        private Grid FillGrid(int rows, int cols, List<List<Rectangle>> _listOfRectangles, Style style, Grid gridName)
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

            return gridName;
        }

        private List<List<Rectangle>> CreateGrid(int rows, int cols, List<List<Rectangle>> _listOfRectangles, Style style, Grid gridName)
        {
            gridName = CreateRowsDefinition(rows, gridName);
            gridName = CreateColumnsDefinition(cols, gridName);
            FillGrid(rows, cols, _listOfRectangles, style, gridName);

            return _listOfRectangles;
        }

        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            _startViewModel = new TetrisStartViewModel();
            DataContext = _startViewModel;

            textBoxNext.Visibility = Visibility.Visible;

            ButtonPauseActivate();


            if (_startViewModel.IsPaused)
            {
                _startViewModel.IsPaused = true;
            }

            if (!timer.Enabled)
            {
                timer.Start();
            }

            _startViewModel.Start(_listOfRectangles, _listOfNextRectangles, ref _previousShapeCoordinate);
        }

        private void ButtonPauseActivate()
        {
            GradientBrush buttonPauseBackground = this.FindResource("GrayGradientBrush") as GradientBrush;
            buttonPause.Opacity = 1;
            buttonPause.IsEnabled = true;
            buttonPause.Background = buttonPauseBackground;
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

                    if (_startViewModel.IsPaused == false && _startViewModel.MovingShape != null)
                    {
                        _startViewModel.SetGameOnPause();
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
            if (_startViewModel.IsEndOfGame)
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
            if (_startViewModel.MovingShape == null || _startViewModel.IsEndOfGame || _startViewModel.IsPaused)
            {
                return;
            }

            _listOfRectangles = _startViewModel.Filler.ClearPreviousShape(_previousShapeCoordinate, _listOfRectangles);

            MoveShape(key);

            bool isCollision = false;

            if (_startViewModel.MovingShape.Points.Exists(p => _startViewModel.Filler.ListOfAllPoints.Exists(point => point.X == p.X + 1 && p.Y == point.Y)))
            {
                isCollision = true;
            }

            _startViewModel.Filler.ListOfAllPoints.AddRange(_startViewModel.MovingShape.Points);
            _listOfRectangles = _startViewModel.Filler.DrawShape(_startViewModel.MovingShape, _listOfRectangles);
            _previousShapeCoordinate = _startViewModel.MovingShape.Points.ConvertAll(p => (Coordinate)p.Clone());

            CheckCollisionLastRow(isCollision);
        }

        public void MoveShape(Key key)
        {
            switch (key)
            {
                case Key.Left:
                    if (_startViewModel.MovingShape.movements.CanMove(Key.Left, _startViewModel.Filler.ListOfAllPoints, _startViewModel.MovingShape.Points))
                    {
                        _startViewModel.MovingShape.movements.Move(Key.Left, _startViewModel.MovingShape.Points);
                    }
                    break;
                case Key.Right:
                    if (_startViewModel.MovingShape.movements.CanMove(Key.Right, _startViewModel.Filler.ListOfAllPoints, _startViewModel.MovingShape.Points))
                    {
                        _startViewModel.MovingShape.movements.Move(Key.Right, _startViewModel.MovingShape.Points);
                    }
                    break;
                case Key.Down:
                    if (_startViewModel.MovingShape.movements.CanMove(Key.Down, _startViewModel.Filler.ListOfAllPoints, _startViewModel.MovingShape.Points))
                    {
                        timer.Stop();
                        _startViewModel.MovingShape.movements.Move(Key.Down, _startViewModel.MovingShape.Points);
                        timer.Start();
                    }
                    break;
                case Key.Up:
                    if (_startViewModel.MovingShape.movements.CanRotate(_startViewModel.Filler.ListOfAllPoints, _startViewModel.MovingShape.Points) && !_startViewModel.IsPaused)
                    {
                        _startViewModel.MovingShape.movements.Rotate(_startViewModel.MovingShape.Points);
                    }
                    break;
            }
        }

        public void CheckCollisionLastRow( bool isCollision)
        {
            if (_startViewModel.MovingShape.Points.Exists(p => _startViewModel.Filler.ListOfAllPoints.Exists(point => (p.X + 1) == TetrisStartViewModel.ROWS || isCollision == true)))
            {
                if (!firstCheck)
                {
                    int lastFoundedLine = 0;
                    int delRows = _startViewModel.CheckRowsForDeleting(ref lastFoundedLine);

                    if  (delRows > 0)
                    {
                        int firstFoundedLine = lastFoundedLine - delRows + 1;

                        _startViewModel.RemoveLines(delRows, firstFoundedLine, _listOfRectangles);
                        _startViewModel.UpdateScore(delRows);
                        _startViewModel.LevelUp();
                        timer.Interval = _startViewModel.TimeOut;
                    }

                    if (_startViewModel.CreateNextShape(_listOfRectangles, _listOfNextRectangles, ref _previousShapeCoordinate))
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateMainGrid();

            InitializeTimer();
        }

       private void InitializeTimer()
        {
            timer = new Timer
            {
                Interval = _startViewModel.TimeOut,
            };

            timer.Elapsed += MoveDownByThread;
        }

        private void Button__EndGame_Click(object sender, RoutedEventArgs e)
        {
            overlay.Visibility = Visibility.Collapsed;

            buttonPause.Opacity = 0.65;
            buttonPause.IsEnabled = false;
            buttonPause.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#626362"));
        }
    }
}