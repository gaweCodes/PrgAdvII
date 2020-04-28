using System;

namespace OverridingEquals
{
    public sealed class OrderedPair : IPair, IEquatable<OrderedPair>
    {
        public object First { get; set; }
        public object Second { get; set; }
        public OrderedPair(object first, object second)
        {
            First = first;
            Second = second;
        }
        public override bool Equals(object obj) => Equals(obj as OrderedPair);
        public bool Equals(OrderedPair other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return First.Equals(other.First) && Second.Equals(other.Second);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;
                var hash = hashingBase;
                hash = (hash * hashingMultiplier) ^ (First?.GetHashCode() ?? 0);
                hash = (hash * hashingMultiplier) ^ (Second?.GetHashCode() ?? 0);
                return hash;
            }
        }
        public static bool operator ==(OrderedPair pair1, OrderedPair pair2)
        {
            if (ReferenceEquals(pair1, pair2))
                return true;
            return pair1?.Equals(pair2) == true;
        }
        public static bool operator !=(OrderedPair pair1, OrderedPair pair2) => !(pair1 == pair2);
    }
}
