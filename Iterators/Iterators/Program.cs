using System;

namespace Iterators
{
    internal static class Program
    {
        private static void Main()
        {
            foreach (var i in Math.Fibonacci(8)) Console.WriteLine($"{i}");

            Console.WriteLine("Task 2");
            var myColl = new CityCollection();
            
            foreach (var s in myColl) Console.WriteLine(s);
            foreach (var s in myColl.Reverse) Console.WriteLine(s);

            Console.WriteLine("Task 2");

            var list1 = new RecursiveList<string>();
            list1.Append("Hallo");
            list1.Append(".NET");
            list1.Append("C#");
            list1.Append("---");

            Console.WriteLine("RecursiveList.Traverse");
            foreach (var s in list1.Traverse) Console.WriteLine(s);
            foreach (var s in list1.Inverse) Console.WriteLine(s);
            list1.ForEach(Console.WriteLine);

            var list2 = new RecursiveList<string>();
            Console.WriteLine("RecursiveList.Traverse (leer)");
            foreach (var s in list2.Traverse) Console.WriteLine(s);
            Console.WriteLine("RecursiveList.Inverse (leer)");
            foreach (var s in list2.Inverse) Console.WriteLine(s);
            Console.WriteLine("RecursiveList.ForEach (leer)");
            list2.ForEach(Console.WriteLine);

            Console.ReadLine();
        }
    }
}
