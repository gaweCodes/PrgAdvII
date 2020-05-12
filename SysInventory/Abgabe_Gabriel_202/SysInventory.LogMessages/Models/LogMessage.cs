using System;
using DuplicateCheckerLib;

namespace SysInventory.LogMessages.Models
{
    public sealed class LogMessage : IEntity, IEquatable<LogMessage>
    {
        public Guid Id { get; set; }
        public string PoD { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }
        public int Severity { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public override bool Equals(object obj) => Equals(obj as LogMessage);
        public bool Equals(LogMessage other)
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
                return (hash * hashingMultiplier) ^ Message.GetHashCode();
            }
        }
        public static bool operator ==(LogMessage logMessageA, LogMessage logMessageB)
        {
            if (ReferenceEquals(logMessageA, logMessageB))
                return true;
            return logMessageA?.Equals(logMessageB) == true;
        }
        public static bool operator !=(LogMessage logMessageA, LogMessage logMessageB) => !(logMessageA == logMessageB);
    }
}