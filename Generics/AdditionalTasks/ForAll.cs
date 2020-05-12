using System;
using System.Collections.Generic;

namespace AdditionalTasks
{
    public static partial class Operators
    {
        public static void ForAll<T>(IEnumerable<T> array, Predicate<T> p, Action<T> a)
        {
            foreach (var item in array)
            {
                if (p(item))
                    a(item);
            }
        }
        public static void TestForAll() => ForAll(
                new[] { 1, 2, 3, 4 },
                i => i > 2,
                Console.WriteLine
            );
    }
}
