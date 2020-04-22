using System;

namespace SysInventory.LogMessages.Models
{
    public class LogMessage
    {
        public Guid Id { get; set; }
        public string PoD { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }
        public int Severity { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
    }
}
