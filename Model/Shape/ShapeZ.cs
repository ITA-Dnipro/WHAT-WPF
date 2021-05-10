using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tetris.Model.Shape
{
    class ShapeZ : BaseShape
    {
        public override void Create(int startX, int startY)
        {
            startY--;

            base.Points.Add(new Coordinate(startX, startY));
            base.Points.Add(new Coordinate(startX, startY + 1));
            base.Points.Add(new Coordinate(startX + 1, startY + 1));
            base.Points.Add(new Coordinate(startX + 1, startY + 2));

            LinearGradientBrush gradient = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0.5),
                EndPoint = new Point(0.5, 1)
            };

            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#f0291a"), 0));
            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#f0291a"), 0.2));
            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#cf2f23"), 1));

            SetColor(gradient);
        }
    }
}
