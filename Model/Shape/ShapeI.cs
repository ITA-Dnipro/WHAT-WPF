using System.Windows;
using System.Windows.Media;

namespace Tetris.Model.Shape
{
    class ShapeI : BaseShape
    {
        public override void Create(int startX, int startY)
        {
            startY--;

            base.Points.Add(new Coordinate(startX, startY - 1));
            base.Points.Add(new Coordinate(startX, startY));
            base.Points.Add(new Coordinate(startX, startY + 1));
            base.Points.Add(new Coordinate(startX, startY + 2));

            LinearGradientBrush gradient = new LinearGradientBrush();
            gradient.StartPoint = new Point(0, 0.5);
            gradient.EndPoint = new Point(0.5, 1);

            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#4ae0e0"), 0));
            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#4ae0e0"), 0.4));
            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#34b4d1"), 1));

            SetColor(gradient);
        }
    }
}
