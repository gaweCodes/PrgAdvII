using System;
using System.Collections;
using System.Collections.Generic;

namespace ExtensionMethods_QueryOperators
{
    public static class EnumerableExtensions
    {
        public static void ZbWForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var sourceItem in source) action(sourceItem);
        }
        public static IEnumerable<T> ZbWWhere<T>(this IEnumerable<T> source, Predicate<T> wherePredicate)
        {
            foreach (var sourceItem in source)
            {
                if (wherePredicate(sourceItem)) yield return sourceItem;
            }
        }
        public static IEnumerable<T> ZbWOfType<T>(this IEnumerable source)
        {
            foreach (var item in source)
            {
                if (item is T) yield return (T)item;
            }
        }
        public static List<T> ZbWToList<T>(this IEnumerable<T> source) => new List<T>(source);
        public static int ZbWSum<T>(this IEnumerable<T> source, Func<T, int> selector)
        {
            var sum = 0;
            foreach (var item in source)
            {
                sum += selector(item);
            }
            return sum;
        }
    }
}
