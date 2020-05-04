using System;

namespace Task1_3_Nullable
{
    internal static class Program
    {
        private static void Main()
        {
            int a = 0;
            bool b = false;
            int? c = 10;
            bool? d = null;
            int? e = null;
            int? x = c + a;
            int? x2 = a + null;
            bool x3 = a < c;
            bool x4 = a + null < c;
            bool x5 = a > null; // false
            int? x6 = (a + c - e) * 9898 + 1000;
            bool? x7 = d;
            bool x8 = d == d;
            int x9 = c ?? 1000;
            int x10 = e ?? 1000;
            Console.WriteLine("int? " + x);
            Console.WriteLine("int? " + x2);
            Console.WriteLine("bool " + x3);
            Console.WriteLine("bool " + x4);
            Console.WriteLine("bool " + x5);
            Console.WriteLine("int? " + x6);
            Console.WriteLine("bool? " + x7);
            Console.WriteLine("bool " + x8);
            Console.WriteLine("int " + x9);
            Console.WriteLine("int " + x10);
            Console.ReadKey();
        }
    }
}
