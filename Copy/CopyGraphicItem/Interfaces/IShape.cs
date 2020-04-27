using System.Drawing;

namespace CopyGraphicItem.Interfaces
{
    internal interface IShape : IGraphicItem
    {
        Color AreaColor { get; set; }
    }
}
