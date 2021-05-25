using System.Windows;
using System.Windows.Media;

namespace Tetris.Models.Shape
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

            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#c93126"), 0));
            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#c93126"), 0.2));
            gradient.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#b52218"), 1));

            SetColor(gradient);
        }
    }
}
