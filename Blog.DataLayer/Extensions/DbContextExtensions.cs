using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blog.DataLayer.Extensions
{
    public static class DbContextExtensions
    {
        public static void TryUpdateManyToMany<T, TKey>(this DbContext dbContext, IEnumerable<T> oldItems, IEnumerable<T> newItems, Func<T, TKey> getKey) where T : class
        {
            dbContext.Set<T>().RemoveRange(oldItems.Except(newItems, getKey));
            dbContext.Set<T>().AddRange(newItems.Except(oldItems, getKey));
        }

        private static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other, Func<T, TKey> getKeyFunc)
        {
            return items
                .GroupJoin(other, getKeyFunc, getKeyFunc, (item, tempItems) => new { item, tempItems })
                .SelectMany(t => t.tempItems.DefaultIfEmpty(), (t, temp) => new { t, temp })
                .Where(t => ReferenceEquals(null, t.temp) || t.temp.Equals(default(T)))
                .Select(t => t.t.item);
        }
    }
}
