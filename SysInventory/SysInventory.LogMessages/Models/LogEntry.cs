using System;
using System.Data.Linq.Mapping;

namespace SysInventory.LogMessages.Models
{
    [Table(Name = "v_LogEntries")]
    public sealed class LogEntry : IEquatable<LogEntry>, ILogEntry
    {
        [Column(Name = "Id", IsPrimaryKey = true)]
        public Guid Id { get; set; }
        [Column(Name = "pod")]
        public string PoD { get; set; }
        [Column(Name = "location")]
        public string Location { get; set; }
        [Column(Name = "Hostname")]
        public string Hostname { get; set; }
        [Column(Name = "Severity")]
        public int Severity { get; set; }
        public DateTime Timestamp { get; set; }
        [Column(Name = "Message")]
        public string Message { get; set; }
        public override bool Equals(object obj) => Equals(obj as LogEntry);
        public bool Equals(LogEntry other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Severity, other.Severity) && string.Equals(Message, other.Message);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;
                var hash = hashingBase;
                hash = (hash * hashingMultiplier) ^ Severity.GetHashCode();
                return (hash * hashingMultiplier) ^ (Message != null ? Message.GetHashCode() : 0);
            }
        }
        public static bool operator ==(LogEntry logMessageA, LogEntry logMessageB)
        {
            if (ReferenceEquals(logMessageA, logMessageB))
                return true;
            return logMessageA?.Equals(logMessageB) == true;
        }
        public static bool operator !=(LogEntry logMessageA, LogEntry logMessageB) => !(logMessageA == logMessageB);
        public override string ToString() =>
            $"Id: {Id}{Environment.NewLine}PoD: {PoD}{Environment.NewLine}Location: {Location}{Environment.NewLine}Hostname: {Hostname}{Environment.NewLine}Serverity: {Severity}{Environment.NewLine}Timestamp: {Timestamp:dd.MM.yyyy}{Environment.NewLine}Message: {Message}";
    }
}