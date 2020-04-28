using System;

namespace OverridingEquals
{
    public sealed class UnorderedPair : IPair, IEquatable<UnorderedPair>
    {
        public object First { get; set; }
        public object Second { get; set; }
        public UnorderedPair(object first, object second)
        {
            First = first;
            Second = second;
        }
        public override bool Equals(object obj) => Equals(obj as UnorderedPair);
        public bool Equals(UnorderedPair other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(First, other.First) && Equals(Second, other.Second) ||
                   Equals(First, other.Second) && Equals(Second, other.First);
        }
        public override int GetHashCode() => (First?.GetHashCode() ?? 0) + (Second?.GetHashCode() ?? 0);
        public static bool operator ==(UnorderedPair pair1, UnorderedPair pair2)
        {
            if (ReferenceEquals(pair1, pair2))
                return true;
            return pair1?.Equals(pair2) == true;
        }
        public static bool operator !=(UnorderedPair pair1, UnorderedPair pair2) => !(pair1 == pair2);
    }
}
