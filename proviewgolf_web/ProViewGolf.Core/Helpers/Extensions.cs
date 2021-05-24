using System;
using System.Collections.Generic;
using System.Linq;

namespace ProViewGolf.Core.Helpers
{
    public static class Extensions
    {
        public static IEnumerable<TSource> NotAssigned<TSource, TKey, TTarget>(this IEnumerable<TSource> source,
            Func<TSource, TKey> sourceKeySelector, IEnumerable<TTarget> target, Func<TTarget, TKey> targetKeySelector)
        {
            var targetValues = new HashSet<TKey>(target.Select(targetKeySelector));

            return source.Where(sourceValue => targetValues.Contains(sourceKeySelector(sourceValue)) == false);
        }

        public static float Average<TSource>(this List<TSource> source, Func<TSource, float?> selector, int digits = -1)
        {
            if (source == null || source.Count == 0) return 0;
            var total = source.Sum(selector.Invoke) ?? 0;

            var avg = total / source.Count;

            return digits >= 0 ? avg.Round(digits) : avg;
        }

        public static float Round(this float f, int digits)
        {
            return (float) Math.Round(f, digits);
        }

        public static double Round(this double d, int digits)
        {
            return Math.Round(d, digits);
        }
    }
}