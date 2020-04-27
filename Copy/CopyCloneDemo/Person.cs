using System;

namespace CopyCloneDemo
{
    internal class Person : ICloneable
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Address Address;
        public Person(string name, int age, int postalCode, string city)
        {
            Name = name;
            Age = age;
            Address = new Address(postalCode, city);
        }
        public object ShallowCopy() => MemberwiseClone();
        public Person DeepCopy() => new Person(Name, Age, Address.PostalCode, Address.City);
        /// <summary>
        /// Unschön weil man nicht weiss ob DeepClone oder Shallowclone
        /// </summary>
        /// <returns>Cloned object</returns>
        public object Clone()
        {
            return ShallowCopy();
            // return DeepCopy();
        }
    }
}
