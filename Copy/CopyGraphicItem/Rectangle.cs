using System.Drawing;
using CopyGraphicItem.Interfaces;

namespace CopyGraphicItem
{
    internal class Rectangle : IShape
    {
        public Color AreaColor { get; set; }
        public Point2D Location { get; }
        public int Width { get; }
        public int Height { get; }
        public Rectangle(Point2D corner, int width, int height, Color color)
        {
            Height = height;
            Width = width;
            AreaColor = color;
            Location = corner;
        }
        public IGraphicItem CopyShallow() => (IGraphicItem)MemberwiseClone();
        public IGraphicItem CopyDeep() => new Rectangle(new Point2D(Location.X, Location.Y), Width, Height, AreaColor);
    }
}
