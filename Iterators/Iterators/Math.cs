using System.Collections.Generic;

namespace Iterators
{
    internal static class Math
    {
        public static IEnumerable<int> Fibonacci(int n)
        {
            var prev = -1;
            var next = 1;
            for (var i = 0; i < n; i++)
            {
                var sum = prev + next;
                prev = next;
                next = sum;
                yield return sum;
            }
        }
    }
}
