using System;
using System.Linq;
using static System.String;

namespace Task1_2_NET_List
{
    internal static class Program
    {
        private static void Main()
        {
            var names = new[] { "Franscoise", "Bill", "Li", "Sandra", "Gunnar", "Alok", "Hiroyuki", "Maria", "Alessandro", "Raul" };
            var ages = new[] { 45, 19, 28, 23, 18, 9, 108, 72, 30, 35 };
            var list = names.Select((t, x) => new Person(t, ages[x])).ToList();

            list.Sort(ComparePersonsByName);
            
            var p30 = list.FindAll(p => p.Age >= 30);
            p30.ForEach(Console.WriteLine);

            Console.ReadKey();
        }
        private static int ComparePersonsByName(Person p1, Person p2) =>
            Compare(p1.Name, p2.Name, StringComparison.Ordinal);
    }
}
