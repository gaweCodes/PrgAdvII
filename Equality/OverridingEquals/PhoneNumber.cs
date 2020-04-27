using System;

namespace OverridingEquals 
{
    public sealed class PhoneNumber : IEquatable<PhoneNumber>
    {
        public string AreaCode { get; set; }
        public string Exchange { get; set; }
        public string SubscriberNumber { get; set; }
        public override bool Equals(object obj) => Equals(obj as PhoneNumber);
        public bool Equals(PhoneNumber other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(AreaCode, other.AreaCode) && string.Equals(Exchange, other.Exchange) &&
                   string.Equals(SubscriberNumber, other.SubscriberNumber);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;
                var hash = hashingBase;
                hash = (hash * hashingMultiplier) ^ (ReferenceEquals(null, AreaCode) ? AreaCode.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (ReferenceEquals(null, Exchange) ? Exchange.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (ReferenceEquals(null, SubscriberNumber) ? SubscriberNumber.GetHashCode() : 0);
                return hash;
            }
        }
        public static bool operator ==(PhoneNumber numberA, PhoneNumber numberB)
        {
            if (ReferenceEquals(numberA, numberB))
                return true;
            return numberA?.Equals(numberB) == true;
        }
        public static bool operator !=(PhoneNumber phoneNumberA, PhoneNumber phoneNumberB) => !(phoneNumberA == phoneNumberB);
    }
}