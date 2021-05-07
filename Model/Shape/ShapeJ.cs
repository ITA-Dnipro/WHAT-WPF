﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tetris.Model.Shape
{
    class ShapeJ : BaseShape
    {
        public override void Create(int startX, int startY)
        {
            startY--;

            base.Points.Add(new Coordinate(startX + 1, startY));
            base.Points.Add(new Coordinate(startX + 1, startY + 1));
            base.Points.Add(new Coordinate(startX + 1, startY + 2));
            base.Points.Add(new Coordinate(startX, startY));
            base.SetColor(Colors.Blue);
        }
    }
}
