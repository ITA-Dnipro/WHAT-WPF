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

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
          Window window =  Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            window.Top -= 150;
            window.Left -= 150;

            Title = Assembly.GetExecutingAssembly().GetName().Name.ToString() + " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            CreateMainGrid();
        }


        private GameManager _gameManager = new GameManager();
        private BaseShape _nextMovingShape;
        private BaseShape _movingShape;
        private Random _rnd = new Random();
        private Rectangle _oneRectangle = new Rectangle();
        private List<List<Rectangle>> _listOfRectangles = new List<List<Rectangle>>(GameManager.COLUMNS);


        public void CreateMainGrid()
        {  
            Thickness ts = new Thickness(0);

            for (int j = 0; j < GameManager.ROWS; j++)
            {
                _listOfRectangles.Add(new List<Rectangle>());
                for (int i = 0; i < GameManager.COLUMNS; i++)
                {

                    _oneRectangle = new Rectangle
                    {
                        Stretch = Stretch.Fill,
                        Margin = ts,
                        Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#507387")),
                        Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#363835"))
                    };

                    Grid.SetColumn(_oneRectangle, i);
                    Grid.SetRow(_oneRectangle, j);
                    mainGrid.Children.Add(_oneRectangle);

                    _listOfRectangles[j].Add(_oneRectangle);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BaseShape shape = _gameManager.FigureCreator.CreateNewShape();
           _listOfRectangles = _gameManager.Filler.DrawShape(shape, _listOfRectangles);
        }
    }
}
