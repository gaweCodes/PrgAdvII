using System.Collections.Generic;

namespace Task1_4_TypeConstraints
{
    internal static class MyHelpers
    {
        public static TDest CopyTo<TSource, TDest, TElement>(TSource source)
            where TSource : IEnumerable<TElement>
            where TDest : IList<TElement>, new()
        {
            var dest = new TDest();
            foreach (var element in source)
                dest.Add(element);
            return dest;
        }
    }
}
