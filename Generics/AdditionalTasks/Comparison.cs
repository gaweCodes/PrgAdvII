using System;

namespace AdditionalTasks
{
    public static partial class Operators
    {
        public static int DefaultCompare<T>(T x, T y) where T : IComparable<T> => x.CompareTo(y);
        public static void TestComparer()
        {
            Comparison<int> comparer = DefaultCompare;
            Console.WriteLine(comparer(7, 7));
        }
    }
}
