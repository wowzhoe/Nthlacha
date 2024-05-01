using System.Collections.Generic;
using System;

namespace CodeFirst
{
    public static class MapExtension
    {
        public static IList<T> Map<T>(this IList<T> list, Action<T> action)
        {
            for (int i = 0; i < list.Count; i++)
            {
                action(list[i]);
            }
            return list;
        }
        public static IList<T> Map<T>(this IList<T> list, Func<T, T> action)
        where T : struct
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = action(list[i]);
            }
            return list;
        }

        public static IList<T> Map<T>(this IList<T> list, Action<int, T> action)
        {
            for (int i = 0; i < list.Count; i++)
            {
                action(i, list[i]);
            }
            return list;
        }

        public static IList<T> Map<T>(this IList<T> list, Func<int, T, T> action)
        where T : struct
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = action(i, list[i]);
            }
            return list;
        }
    }
}
