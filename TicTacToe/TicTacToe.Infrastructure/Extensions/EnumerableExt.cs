namespace TicTacToe.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExt
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !IsNullOrEmpty(enumerable);
        }

        public static IEnumerable<T> Default<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? new List<T>();
        }

        public static void Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items == null)
            {
                return;
            }

            var cached = items;

            foreach (var item in cached)
            {
                action(item);
            }
        }
    }
}
