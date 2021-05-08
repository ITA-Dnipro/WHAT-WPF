using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public FieldFiller Filler { get; } = new FieldFiller();
        public ShapeCreator FigureCreator { get; } = new ShapeCreator();
        public BaseShape MovingShape { get; set; }
        public BaseShape NextMovingShape { get; set; }

        public void Start(List<List<Rectangle>> _listOfRectangles, List<List<Rectangle>> _listOfNextRectangles,ref List<Coordinate> _previousShapeCoordinate)
        {
            _listOfRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#507387")); }));
            _listOfNextRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364c5c")); }));
            Filler.ListOfAllPoints.Clear();

            MovingShape = FigureCreator.CreateNewShape(GameManager.COLUMNS);

            _previousShapeCoordinate = CreateShape(MovingShape, _listOfRectangles, _previousShapeCoordinate);

            //_listOfRectangles = Filler.DrawShape(MovingShape, _listOfRectangles);
            //Filler.ListOfAllPoints.AddRange(MovingShape.Points);
            //_previousShapeCoordinate = MovingShape.Points.ConvertAll(p => (Coordinate)p.Clone());

             NextMovingShape = FigureCreator.CreateNewShape(4);
            _listOfNextRectangles = Filler.DrawShape(NextMovingShape, _listOfNextRectangles);

        }

        public void CreateNewShape(List<List<Rectangle>> _listOfRectangles, List<List<Rectangle>> _listOfNextRectangles, ref List<Coordinate> _previousShapeCoordinate)
        {
            _listOfNextRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#364c5c")); }));

            MovingShape = NextMovingShape;

            _previousShapeCoordinate = CreateShape(MovingShape, _listOfRectangles, _previousShapeCoordinate);

            //_listOfRectangles = Filler.DrawShape(MovingShape, _listOfRectangles);
            //Filler.ListOfAllPoints.AddRange(MovingShape.Points);
            //_previousShapeCoordinate = MovingShape.Points.ConvertAll(p => (Coordinate)p.Clone());

            NextMovingShape = FigureCreator.CreateNewShape(4);
            _listOfNextRectangles = Filler.DrawShape(NextMovingShape, _listOfNextRectangles);

        }

        public List<Coordinate> CreateShape(BaseShape movingShape, List<List<Rectangle>> _listOfRectangles, List<Coordinate> _previousShapeCoordinate)
        {
            _listOfRectangles = Filler.DrawShape(MovingShape, _listOfRectangles);
            Filler.ListOfAllPoints.AddRange(MovingShape.Points);
            _previousShapeCoordinate = MovingShape.Points.ConvertAll(p => (Coordinate)p.Clone());

            return _previousShapeCoordinate;
        }
    }
}
