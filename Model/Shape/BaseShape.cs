using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Enums;

namespace Tetris.Model.Shape
{
    abstract class BaseShape
    {
        private List<Coordinate> points = new List<Coordinate>();

        public bool CanMove(Arrow side, List<Coordinate> listOfAllShapesCoordinate)
        {
            bool moving = true;
            int border = 0;

            switch (side)
            {
                //case Arrow.Up: border = GameField.COLUMNS - 1;
                //    break;
                case Arrow.Down: border = GameField.ROWS - 1;
                    break;
                case Arrow.Left: border = -1;
                    break;
                case Arrow.Right: border = GameField.COLUMNS - 1;
                    break;
            }

            if (!points.Exists(p => (p.X - 1) < 0))
            {
                if (!listOfAllShapesCoordinate.Exists(allShapes => points.Exists(p => (p.X - 1) == allShapes.X && p.Y == allShapes.Y)))
                {
                    points.ForEach(p => { p.X--; });
                }
            }

            return moving;
        }
    }
}
