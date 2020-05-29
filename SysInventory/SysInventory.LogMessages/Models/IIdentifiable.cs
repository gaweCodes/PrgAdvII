using System;
using System.Data.Linq.Mapping;

namespace SysInventory.LogMessages.Models
{
    public interface IIdentifiable
    {
        [Column(Name = "Id", IsPrimaryKey = true)]
        Guid Id { get; set; }
    }
}
