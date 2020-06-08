using System;
using System.Collections.Generic;

namespace ExtensionMethods_QueryOperators
{
    public static class Program
    {
        private static IEnumerable<string> Cities
        {
            get { return new[] {"Bern", "Basel", "Zürich", "St. Gallen", "Genf"}; }
        }
        private static void Main()
        {
            Console.WriteLine("---------- \r\nZbwForEach");
            TestForEach();
            Console.WriteLine("---------- \r\nZbwWhere");
            TestWhere();
            Console.WriteLine("---------- \r\nZbwOfType");
            TestOfType();
            Console.WriteLine("---------- \r\nZbwToList");
            TestToList();
            Console.WriteLine("---------- \r\nZbwSum");
            TestSum();
            Console.ReadLine();
        }
        private static void TestForEach()
        {
            Console.WriteLine("ZbwForEach / Cities");
            Cities.ZbWForEach(Console.WriteLine);
            Console.WriteLine("ZbwForEach / Cities (Upper-Case)");
            Cities.ZbWForEach(s => Console.WriteLine(s.ToUpper()));
        }
        private static void TestWhere()
        {
            IEnumerable<string> q1 = EnumerableExtensions.ZbWWhere(Cities, s => s.Length < 5);
            IEnumerable<string> q2 = Cities.ZbWWhere(s => s.Length < 5);
            q1.ZbWForEach(Console.WriteLine);
            q2.ZbWForEach(s => Console.WriteLine(s));

            Cities.ZbWWhere(s => s.Length < 5).ZbWForEach(s => Console.WriteLine(s));
        }
        private static void TestOfType()
        {
            object[] objs = { 1, "St. Gallen", true, "Zürich", 7.9, "Bern" };
            IEnumerable<string> q1 = objs.ZbWOfType<string>();
            q1.ZbWForEach(s => Console.WriteLine(s));

        }
        private static void TestToList()
        {
            List<string> res = Cities.ZbWToList();
            res.ZbWForEach(Console.WriteLine);
        }
        private static void TestSum()
        {
            int totalLength = Cities.ZbWSum(c => c.Length);
            Console.WriteLine("TotalLength = {0}", totalLength);
        }
    }
}
