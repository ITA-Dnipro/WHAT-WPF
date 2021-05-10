using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tetris.Model.Shape
{
    class ShapeO : BaseShape
    {
        public override void Create(int startX, int startY)
        {
            startY--;

            Points.Add(new Coordinate(startX, startY));
            Points.Add(new Coordinate(startX, startY + 1));
            Points.Add(new Coordinate(startX + 1, startY));
            Points.Add(new Coordinate(startX + 1, startY + 1));

            LinearGradientBrush gradient = new LinearGradientBrush();
            gradient.StartPoint = new Point(0, 0);
            gradient.EndPoint = new Point(0, 1);

            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#eaed32"), 0));
            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#eaed32"), 0.3));
            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#b3b06f"), 1));

            SetColor(gradient);
        }
    }
}
