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
    class FieldFiller
    {
        public List<Coordinate> ListOfAllPoints { get; set; } = new List<Coordinate>();

        public List<List<Rectangle>> DrawShape(BaseShape shape, List<List<Rectangle>> listOfRectangles)
        {

            shape.Points.ForEach(p => {listOfRectangles = DrawOnePoint(p, listOfRectangles); });

            //listOfNextRectangles.ForEach(l => l.ForEach(r => { r.Fill = new SolidColorBrush(Colors.White); }));
            //_gameManager.GetAllPoints.ForEach(p => { DrawOnePoint(p.X, p.Y, p.PointColor); });
            //if (!_gameManager.IsEndOfGame) _gameManager.MovingShape.Points.ForEach(p => { DrawOnePoint(p.X, p.Y, p.PointColor); });
            //DrawNext();

            return listOfRectangles;
        }

        public List<List<Rectangle>> ClearPreviousShape(List<Coordinate> previousShapeCoordinate, List<List<Rectangle>> listOfRectangles)
        {
            for (int i = 0; i < previousShapeCoordinate.Count; i++)
            {
                listOfRectangles[previousShapeCoordinate[i].X][previousShapeCoordinate[i].Y].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#507387"));

               Coordinate coord = ListOfAllPoints.FirstOrDefault(p=>p.X == previousShapeCoordinate[i].X && p.Y == previousShapeCoordinate[i].Y);
                ListOfAllPoints.RemoveAll(p=>p==coord);
            }

            return listOfRectangles;
        }

        private List<List<Rectangle>> DrawOnePoint(Coordinate coord,  List<List<Rectangle>> listOfRectangles)
        {
            listOfRectangles[coord.X][coord.Y].Fill = new SolidColorBrush(coord.PointColor);

            return listOfRectangles;
        }
    }
}
