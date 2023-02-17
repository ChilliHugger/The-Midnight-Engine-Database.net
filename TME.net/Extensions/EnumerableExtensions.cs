using System.Collections.Generic;
using System.Linq;

namespace TME.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> o) where T:class
            => o.Where(x => x != null)!;
    }
}