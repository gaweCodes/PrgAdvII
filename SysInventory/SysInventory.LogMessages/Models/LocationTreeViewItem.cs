using System.Collections.Generic;

namespace SysInventory.LogMessages.Models
{
    public sealed class LocationTreeViewitem
    {
        public ILocation Item { get; set; }
        public IEnumerable<LocationTreeViewitem> Children { get; set; }
        public override string ToString() => Item.ToString();
    }
}
