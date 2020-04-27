using System;

namespace EqualsDemo
{
    internal sealed class Person : IEquatable<Person>
    {
        public int UniqueId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public override bool Equals(object obj) => Equals(obj as Person);
        public bool Equals(Person other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this , other)) return true;
            return string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName) &&
                   Birthdate.Equals(other.Birthdate);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                const int hashingBase = (int) 2166136261;
                const int hashingMultiplier = 16777619;
                var hash = hashingBase;
                hash = (hash * hashingMultiplier) ^ (ReferenceEquals(null, FirstName) ? FirstName.GetHashCode()  : 0);
                hash = (hash * hashingMultiplier) ^ (ReferenceEquals(null, LastName) ? LastName.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (ReferenceEquals(null, Birthdate) ? Birthdate.GetHashCode() : 0);
                return hash;
            }
        }
        public static bool operator ==(Person personA, Person personB)
        {
            if (ReferenceEquals(personA, personB))
                return true;
            return personA?.Equals(personB) == true;
        }
        public static bool operator !=(Person personA, Person personB) => !(personA == personB);
    }
}
