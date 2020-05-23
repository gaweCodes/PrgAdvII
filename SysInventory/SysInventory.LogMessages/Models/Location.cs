using System;

namespace SysInventory.LogMessages.Models
{
    public sealed class Location : IIdentifiable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PoD { get; set; }
        public Guid PoDId { get; set; }
        public Guid? ParentId { get; set; }
        public override string ToString() => Name;
    }
}
