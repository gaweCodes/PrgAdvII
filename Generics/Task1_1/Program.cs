using System;

namespace Task1_1
{
    internal static class Program
    {
        private static void Main()
        {
            var names = new[] {"Franscoise", "Bill", "Li", "Sandra", "Gunnar", "Alok", "Hiroyuki", "Maria", "Alessandro", "Raul"};
            var ages = new[] {45, 19, 28, 23, 18, 9, 108, 72, 30, 35};
            
            var list = new MySortedList<Person>();
            for (var x = 0; x < names.Length; x++)
                list.Add(new Person(names[x], ages[x]));

            Console.WriteLine("Unsorted List:");
            foreach (var p in list)
                Console.WriteLine(p.ToString());
            list.BubbleSort();
            Console.WriteLine();
            Console.WriteLine("Sorted List:");
            foreach (var p in list)
                Console.WriteLine(p.ToString());
            Console.ReadLine();
        }
    }
}
