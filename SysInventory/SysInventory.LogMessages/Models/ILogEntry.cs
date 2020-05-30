using System;
using DuplicateCheckerLib;

namespace SysInventory.LogMessages.Models
{
    public interface ILogEntry : IIdentifiable, IEntity
    {
        string PoD { get; set; }
        string Location { get; set; }
        string Hostname { get; set; }
        int Severity { get; set; }
        DateTime Timestamp { get; set; }
        string Message { get; set; }
    }
}
