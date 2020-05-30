using System;
using System.Collections.Generic;
using System.Linq;
using SysInventory.LogMessages.Models;

namespace SysInventory.LogMessages.Extensions
{
    internal static class ListExtension
    {
        public static IEnumerable<LocationTreeViewitem> GenerateTree<TK>
        (
            this IEnumerable<ILocation> collection,
            Func<ILocation, TK> idSelector,
            Func<ILocation, TK> parentIdSelector,
            TK rootId = default)
        {
            var list = collection.ToList();
            foreach (var item in list.Where(_ => parentIdSelector(_).Equals(rootId)))
            {
                yield return new LocationTreeViewitem
                {
                    Item = item,
                    Children = list.GenerateTree(idSelector, parentIdSelector, idSelector(item))
                };
            }
        }
    }
}
