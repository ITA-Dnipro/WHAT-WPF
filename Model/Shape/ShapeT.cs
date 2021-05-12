using System.Windows;
using System.Windows.Media;

namespace Tetris.Model.Shape
{
    class ShapeT : BaseShape
    {
        public override void Create(int startX, int startY)
        {
            startY--;

            base.Points.Add(new Coordinate(startX + 1, startY - 1));
            base.Points.Add(new Coordinate(startX + 1, startY));
            base.Points.Add(new Coordinate(startX + 1, startY + 1));
            base.Points.Add(new Coordinate(startX, startY));

            LinearGradientBrush gradient = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0.5),
                EndPoint = new Point(0.5, 1)
            };

            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#9e1af0"), 0));
            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#9e1af0"), 0.2));
            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#7b22b3"), 1));

            SetColor(gradient);
        }
    }
}
