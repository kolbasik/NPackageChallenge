using System;
using System.Collections.Generic;
using System.Linq;

namespace com.mobiquityinc.extensions
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<TResult> Map<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> mapper)
        {
            return source.Select(mapper);
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
                yield return item;
            }
        }
    }
}