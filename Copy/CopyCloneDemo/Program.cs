using System;

namespace CopyCloneDemo
{
    internal static class Program
    {
        private static void Main()
        {
            var p1 = new Person("Thomas Kehl", 39, 9442, "Berneck");
            // Reference Copy
            // var p2 = p1;
            // var p2 = (Person) p1.ShallowCopy();
            var p2 = p1.DeepCopy();

            p1.Age = 40;
            Console.WriteLine(p1.Age);
            Console.WriteLine(p2.Age);

            p1.Name = "Franz Meier";
            Console.WriteLine(p1.Name);
            Console.WriteLine(p2.Name);

            p1.Address.PostalCode = 9000;
            Console.WriteLine(p1.Address.PostalCode);
            Console.WriteLine(p2.Address.PostalCode);

            p1.Address.City = "St. Gallen";
            Console.WriteLine(p1.Address.City);
            Console.WriteLine(p2.Address.City);
            Console.ReadLine();
        }
    }
}
