using System.Windows;
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

            LinearGradientBrush gradient = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1)
            };

            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#2e4ef0"), 0));
            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#1e04c4"), 1));

            SetColor(gradient);
        }
    }
}
