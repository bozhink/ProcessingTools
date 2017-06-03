namespace ProcessingTools.Common.Extensions.Linq
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static class EnumerableExtensions
    {
        public static void ForEach(this IEnumerable source, Action<object> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var item in source)
            {
                action.Invoke(item);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var item in source)
            {
                action.Invoke(item);
            }
        }

        public static Task ForEachAsync(this IEnumerable source, Action<object> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return Task.Run(() =>
            {
                foreach (var item in source)
                {
                    action.Invoke(item);
                }
            });
        }

        public static Task ForEachAsync<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return Task.Run(() =>
            {
                foreach (var item in source)
                {
                    action.Invoke(item);
                }
            });
        }

        public static async Task ForEachAsync(this IEnumerable source, Func<object, Task> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var item in source)
            {
                await action.Invoke(item);
            }
        }

        public static async Task ForEachAsync<T>(this IEnumerable<T> source, Func<object, Task> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var item in source)
            {
                await action.Invoke(item);
            }
        }
    }
}
