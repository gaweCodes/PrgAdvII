//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.DataAccess.Ef
{
    using System;
    using System.Collections.Generic;

    public partial class v_logentries : ILogEntry, IEquatable<v_logentries>
    {
        public System.Guid Id { get; set; }
        public string PoD { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }
        public int Severity { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public override bool Equals(object obj) => Equals(obj as v_logentries);
        public bool Equals(v_logentries other)
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
        public static bool operator ==(v_logentries logMessageA, v_logentries logMessageB)
        {
            if (ReferenceEquals(logMessageA, logMessageB))
                return true;
            return logMessageA?.Equals(logMessageB) == true;
        }
        public static bool operator !=(v_logentries logMessageA, v_logentries logMessageB) => !(logMessageA == logMessageB);
        public override string ToString() =>
            $"Id: {Id}{Environment.NewLine}PoD: {PoD}{Environment.NewLine}Location: {Location}{Environment.NewLine}Hostname: {Hostname}{Environment.NewLine}Serverity: {Severity}{Environment.NewLine}Timestamp: {Timestamp:dd.MM.yyyy}{Environment.NewLine}Message: {Message}";
    }
}
