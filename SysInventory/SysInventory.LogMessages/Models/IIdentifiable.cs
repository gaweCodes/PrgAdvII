using System;

namespace SysInventory.LogMessages.Models
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}
