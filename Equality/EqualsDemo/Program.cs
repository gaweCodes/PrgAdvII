using System;

namespace EqualsDemo
{
    internal static class Program
    {
        private static void Main()
        {
            var gabriel = new Person{Birthdate = DateTime.Parse("29.06.1995"), FirstName ="Gabriel", LastName = "Weibel"};
            Person thomas = null;
            // false
            Console.WriteLine(gabriel.Equals(thomas));
            thomas = new Person { Birthdate = DateTime.Parse("29.06.1995"), FirstName = "Thomas", LastName = "Canal" };
            // false
            Console.WriteLine(gabriel.Equals(thomas));
            thomas = new Person { Birthdate = DateTime.Parse("29.06.1995"), FirstName = "Gabriel", LastName = "Weibel" };
            // true
            Console.WriteLine(gabriel.Equals(thomas));
            Console.ReadLine();
        }
    }
}
