using System.Drawing;
using CopyGraphicItem.Interfaces;

namespace CopyGraphicItem
{
    internal class Circle : IShape
    {
        public Point2D Location { get; }
        public Color AreaColor { get; set; }
        public int Radius { get; }
        public Circle(Point2D center, int radius, Color color)
        {
            Location = center;
            Radius = radius;
            AreaColor = color;
        }
        public IGraphicItem CopyShallow() => (IGraphicItem) MemberwiseClone();
        public IGraphicItem CopyDeep() => new Circle(new Point2D(Location.X, Location.Y), Radius, AreaColor);
    }
}
