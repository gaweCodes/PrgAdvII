using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class QueryOperators
    {
        public static IEnumerable<int> ZbwMultipleOf(this IEnumerable<int> source, int factor)
        {
            foreach (var item in source)
            {
                if (item % factor == 0)
                    yield return item;
            }
        }
        public static IEnumerable<T> ZbwWhere<T>(this IEnumerable<T> source, Predicate<T> filter)
        {
            foreach (var item in source)
            {
                if (filter(item)) 
                    yield return item;
            }
        }
    }
}
