// <copyright file="EnumerableExtensions.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Extensions for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Performs the specified action on each element of the <see cref="IEnumerable"/>.
        /// </summary>
        /// <param name="source">Source collection.</param>
        /// <param name="action">The <see cref="Action{Object}"/> delegate to perform on each element of the <see cref="IEnumerable"/>.</param>
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

        /// <summary>
        /// Performs the specified action on each element of the <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of source collection.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="action">The <see cref="Action{T}"/> delegate to perform on each element of the <see cref="IEnumerable{T}"/>.</param>
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

        /// <summary>
        /// Performs asynchronously the specified action on each element of the <see cref="IEnumerable"/>.
        /// </summary>
        /// <param name="source">Source collection.</param>
        /// <param name="action">The <see cref="Action{Object}"/> delegate to perform on each element of the <see cref="IEnumerable"/>.</param>
        /// <returns>Task</returns>
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

        /// <summary>
        /// Performs asynchronously the specified action on each element of the <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of source collection.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="action">The <see cref="Action{T}"/> delegate to perform on each element of the <see cref="IEnumerable{T}"/>.</param>
        /// <returns>Task</returns>
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

        /// <summary>
        /// Performs asynchronously the specified action on each element of the <see cref="IEnumerable"/>.
        /// </summary>
        /// <param name="source">Source collection.</param>
        /// <param name="action">The <see cref="Func{Object,Task}"/> delegate to perform on each element of the <see cref="IEnumerable"/>.</param>
        /// <returns>Task</returns>
        public static Task ForEachAsync(this IEnumerable source, Func<object, Task> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            List<Task> tasks = new List<Task>();
            foreach (var item in source)
            {
                tasks.Add(action.Invoke(item));
            }

            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// Performs asynchronously the specified action on each element of the <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type of source collection.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="action">The <see cref="Func{Object,Task}"/> delegate to perform on each element of the <see cref="IEnumerable{T}"/>.</param>
        /// <returns>Task</returns>
        public static Task ForEachAsync<T>(this IEnumerable<T> source, Func<object, Task> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            List<Task> tasks = new List<Task>();
            foreach (var item in source)
            {
                tasks.Add(action.Invoke(item));
            }

            return Task.WhenAll(tasks);
        }

        /// <summary>
        /// Evaluates logical AND operation over all members of the source collection.
        /// </summary>
        /// <param name="source">Source collection to be evaluated.</param>
        /// <returns>Result of the logical AND operation.</returns>
        public static bool LogicalAnd(this IEnumerable<bool> source)
        {
            if (source == null || !source.Any())
            {
                return false;
            }

            foreach (var item in source)
            {
                if (!item)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Evaluates logical OR operation over all members of the source collection.
        /// </summary>
        /// <param name="source">Source collection to be evaluated.</param>
        /// <returns>Result of the logical OR operation.</returns>
        public static bool LogicalOr(this IEnumerable<bool> source)
        {
            if (source == null || !source.Any())
            {
                return false;
            }

            foreach (var item in source)
            {
                if (item)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
