﻿namespace CopyGraphicItem.Interfaces
{
    internal interface IGraphicItem
    {
        Point2D Location { get; }
        IGraphicItem CopyShallow();
        IGraphicItem CopyDeep();
    }
}
