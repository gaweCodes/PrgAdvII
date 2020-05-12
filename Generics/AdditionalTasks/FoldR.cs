using System;
using System.Collections.Generic;
using System.Linq;

namespace AdditionalTasks
{
    public static partial class Operators
    {
        public delegate T FoldHandler<T>(T p1, T p2);
        public static T FoldR<T>(T start, IEnumerable<T> elements, FoldHandler<T> foldHandler) =>
            elements.Aggregate(start, (current, element) => foldHandler(current, element));
        public static T Smallest<T>(T maxElement, IEnumerable<T> elements, Comparison<T> comparison) =>
            FoldR(maxElement, elements, (p1, p2) => comparison(p1, p2) <= 0 ? p1 : p2);
        public static T Smallest<T>(T maxElement, IEnumerable<T> elements) where T : IComparable<T> =>
            Smallest(maxElement, elements, DefaultCompare);
        public static void TestFold()
        {
            var array = new[] { 1, 23, 2, -10, 10, -1000, 23 };
            Console.WriteLine(
                Smallest(
                    int.MaxValue,
                    array,
                    (v1, v2) => ((IComparable) v1).CompareTo(v2)
                )
            );
            Console.WriteLine(
                Smallest(
                    int.MaxValue,
                    array
                )
            );
        }
    }
}
