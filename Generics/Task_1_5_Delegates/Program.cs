using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Task1_5_Delegates
{
    // TODO: Ersetzen Sie das Delegate durch ein generisches Delegate
    // delegate int Comparer(object x, object y);
    delegate int Comparer<in T>(T a, T b);

    internal static class Program
    {
        private static int CompareFraction(Fraction f1, Fraction f2)
        {
            var fract1 = (float)f1.A / f1.B;
            var fract2 = (float)f2.A / f2.B;
            if (fract1 < fract2) return -1;
            return fract1 > fract2 ? 1 : 0;
        }
        private static int CompareString(string x, string y) => string.Compare(x, y, StringComparison.Ordinal);
        private static void Sort<T>(T[] a, Comparer<T> compare)
        {
            Debug.Assert(compare != null && compare.GetInvocationList().Length == 1, "Genau eine Vergleichsfunktion");

            for (var i = 0; i < a.Length - 1; i++)
            {
                var min = i;
                for (var j = i + 1; j < a.Length; j++)
                {
                    if (compare(a[j], a[min]) < 0) min = j;
                }

                if (min == i) continue;
                T x = a[i];
                a[i] = a[min];
                a[min] = x;
            }
        }
        private static void Main()
        {
            var a = new List<Fraction> { new Fraction(1, 2), new Fraction(3, 4), new Fraction(4, 8), new Fraction(8, 3) };
            var b = new List<string> { "pears", "apples", "oranges", "bananas", "plums" };
            
            a.Sort(CompareFraction);
            a.ForEach(f => Console.WriteLine(f + " "));
            Console.WriteLine();

            b.Sort(CompareString);
            b.ForEach(s => Console.WriteLine(s + " "));
            Console.WriteLine();

            var x = a.ConvertAll(f => f.ToString());
            x.ForEach(s => Console.WriteLine(s + " "));

            Console.ReadLine();
        }
    }
}
