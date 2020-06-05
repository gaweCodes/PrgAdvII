using System;
using DuplicateCheckerLib;

namespace SysInventory.LogMessages.Models
{
    public interface ILocation : IIdentifiable, IEntity
    {
        string Name { get; set; }
        Guid PoDId { get; set; }
        Guid? ParentId { get; set; }
        int? Level { get; set; }
    }
}
