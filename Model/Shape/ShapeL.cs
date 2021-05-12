using System.Windows;
using System.Windows.Media;

namespace Tetris.Model.Shape
{
    class ShapeL : BaseShape
    {
        public override void Create(int startX, int startY)
        {
            startY--;

            base.Points.Add(new Coordinate(startX + 1, startY));
            base.Points.Add(new Coordinate(startX + 1, startY + 1));
            base.Points.Add(new Coordinate(startX + 1, startY + 2));
            base.Points.Add(new Coordinate(startX, startY + 2));

            LinearGradientBrush gradient = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0.5),
                EndPoint = new Point(0.5, 1)
            };

            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#e3931b"), 0));
            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#e3931b"), 0.2));
            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#bd842f"), 1));

            SetColor(gradient);
        }
    }
}
