using System;
using System.Collections.Generic;

namespace Extensions
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("---------- \r\nCamelCase");
            TestCamelCase();
            Console.WriteLine("---------- \r\nToStringSafe");
            TestToStringSafe();

            int[] numbers = { 1, 4, 2, 9, 13, 8, 9, 0, -6, 12 };
            string[] cities = { "Bern", "Basel", "Zürich", "Rapperswil", "Genf" };

            Console.WriteLine("---------- \r\nOperator 'ZbwMultipleOf' / Vielfaches von 4 auf 'numbers'");
            var multiple4 = numbers.ZbwMultipleOf(4);
            foreach (var i in multiple4) Console.WriteLine(i);
            Console.WriteLine("---------- \r\nOperator 'ZbwMultipleOf' / Vielfaches von 2 und 3 auf 'numbers'");
            var multiple23 = numbers.ZbwMultipleOf(2).ZbwMultipleOf(3);
            foreach (var i in multiple23) Console.WriteLine(i);
            var citiesB = cities.ZbwWhere(s => s.StartsWith("B"));
            foreach (var s in citiesB) Console.WriteLine(s);

            var citiesContainingEAndMinimumLengthOb5 =
                cities.ZbwWhere(s => s.ToString().Contains("e")).ZbwWhere(x => x.Length >= 5);
            foreach (var s in citiesContainingEAndMinimumLengthOb5) Console.WriteLine(s);

            var multiple2AndHigherOrEqual5WhereAndMultipleOf = numbers.ZbwMultipleOf(2).ZbwWhere(x => x >= 5);
            foreach (var s in multiple2AndHigherOrEqual5WhereAndMultipleOf) Console.WriteLine(s);
            // var multiple2AndHigherOrEqual5Where = numbers.ZbwWhere(x => x >= 5 && x % 2 == 0);
            var multiple2AndHigherOrEqual5Where = numbers.ZbwWhere(x => x >= 5).ZbwWhere(y=> y % 2 == 0);
            foreach (var s in multiple2AndHigherOrEqual5Where) Console.WriteLine(s);
        }
        private static void TestCamelCase()
        {
            var identifiers = new[]
            {
                "do_something",
                "find_all_objects",
                "get_last_dict_entry"
            };
            foreach (var s in identifiers) Console.WriteLine($"{s} becomes: {MyExtensions.ToCamelCase(s)}");
            foreach (var s in identifiers) Console.WriteLine($"{s} becomes: {1}", s, s.ToCamelCase());
        }
        private static void TestToStringSafe()
        {
            var a = "string abc";
            Console.WriteLine(a.ToStringSafe());
            a = null;
            Console.WriteLine(a.ToStringSafe());
        }
    }
}