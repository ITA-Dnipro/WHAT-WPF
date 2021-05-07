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
                listOfRectangles[previousShapeCoordinate[i].Y][previousShapeCoordinate[i].X].Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#507387"));
            }

            return listOfRectangles;
        }

        private List<List<Rectangle>> DrawOnePoint(Coordinate coord,  List<List<Rectangle>> listOfRectangles)
        {
            listOfRectangles[coord.Y][coord.X].Fill = new SolidColorBrush(coord.PointColor);

            return listOfRectangles;
        }
    }
}
