using System;

namespace Task1_2_NET_List
{
    internal sealed class Person : IComparable<Person>
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
        public int CompareTo(Person p)
        {
            if (p != null)
                return Age - p.Age;
            throw new Exception("p has wrong type");
        }
        public override string ToString() => Name + ":" + Age;
    }
}
