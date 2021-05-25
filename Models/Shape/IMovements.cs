using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Tetris.Models.Shape
{
    interface IMovements
    {
        bool CanMove(Key side, List<Coordinate> listOfAllShapesCoordinate, List<Coordinate> points);
        void Move(Key side, List<Coordinate> points);
        bool CanRotate(List<Coordinate> listOfAllShapesCoordinate, List<Coordinate> points);
        void Rotate(List<Coordinate> points);
    }
}

