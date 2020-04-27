using System.Drawing;
using CopyGraphicItem.Interfaces;

namespace CopyGraphicItem
{
    internal class TextBox : Rectangle, IText
    {
        public string Text { get; }
        public Color TextColor { get; }
        public TextBox(Point2D location, int width, int height, Color areaColor, string text, Color textColor) : base(location, width, height, areaColor)
        {
            Text = text;
            TextColor = textColor;
        }
    }
}
