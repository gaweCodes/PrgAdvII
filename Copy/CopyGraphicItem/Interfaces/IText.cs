using System.Drawing;

namespace CopyGraphicItem.Interfaces
{
    internal interface IText : IGraphicItem
    {
        string Text { get; }
        Color TextColor { get; }
    }
}
