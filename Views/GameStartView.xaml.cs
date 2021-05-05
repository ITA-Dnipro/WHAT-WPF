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
            Title = Assembly.GetExecutingAssembly().GetName().Name.ToString() + " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            CreateMainGrid();
            // DrawAllCells();
        }

        private Rectangle _oneRectangle = new Rectangle();
        private List<List<Rectangle>> _listOfRectangles = new List<List<Rectangle>>(GameField.COLUMNS);
        private List<List<Rectangle>> _listOfNextRectangles = new List<List<Rectangle>>(4);
        // private GameField _gameManager;

        public void CreateMainGrid()
        {
            Thickness ts = new Thickness(0);

            for (int j = 0; j < GameField.ROWS; j++)
            {
                _listOfRectangles.Add(new List<Rectangle>());
                for (int i = 0; i < GameField.COLUMNS; i++)
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
    }
}
