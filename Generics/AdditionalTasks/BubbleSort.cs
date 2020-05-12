using System;

namespace AdditionalTasks
{
    public static partial class Operators
    {
        public static void BubbleSort<T>(int length, Func<int, T> getter, Action<int, T> setter, Comparison<T> comparer)
        {
            for (var io = 0; io < length - 1; io++)
            {
                for (var ii = 0; ii < length - io - 1; ii++)
                {
                    var e1 = getter(ii);
                    var e2 = getter(ii + 1);
                    if (comparer(e1, e2) <= 0) continue;
                    setter(ii, e2);
                    setter(ii + 1, e1);
                }
            }
        }
        public static void TestSort()
        {
            var array = new[] { 1, 23, 2, -10, 10, 1000, 23, 100000, -12 };
            BubbleSort(array.Length,
                i => array[i],
                    (i, e) => array[i] = e,
                    DefaultCompare<int>);
            foreach (var e in array) Console.WriteLine(e);
        }
    }
}
