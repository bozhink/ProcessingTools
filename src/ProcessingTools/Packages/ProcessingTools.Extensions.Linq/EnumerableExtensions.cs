// <copyright file="EnumerableExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
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
        /// <returns>Task.</returns>
        public static Task ForEachAsync(this IEnumerable source, Action<object> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
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
        /// <returns>Task.</returns>
        public static Task ForEachAsync<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
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
        /// <returns>Task.</returns>
        public static Task ForEachAsync(this IEnumerable source, Func<object, Task> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
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
        /// <returns>Task.</returns>
        public static Task ForEachAsync<T>(this IEnumerable<T> source, Func<object, Task> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
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
        /// Evaluates logical AND operation over members of the source collection.
        /// </summary>
        /// <param name="source">Source collection to be evaluated.</param>
        /// <returns>Result of the logical AND operation.</returns>
        public static bool LogicalAnd(this IEnumerable<bool> source)
        {
            if (source is null || !source.Any())
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
        /// Evaluates logical AND operation over members of the source collection.
        /// </summary>
        /// <param name="source">Source collection to be evaluated.</param>
        /// <returns>Result of the logical AND operation.</returns>
        public static bool LogicalAnd(this IEnumerable<Func<bool>> source)
        {
            if (source is null || !source.Any())
            {
                return false;
            }

            foreach (var item in source)
            {
                if (!item())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Evaluates logical OR operation over members of the source collection.
        /// </summary>
        /// <param name="source">Source collection to be evaluated.</param>
        /// <returns>Result of the logical OR operation.</returns>
        public static bool LogicalOr(this IEnumerable<bool> source)
        {
            if (source is null || !source.Any())
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

        /// <summary>
        /// Evaluates logical OR operation over members of the source collection.
        /// </summary>
        /// <param name="source">Source collection to be evaluated.</param>
        /// <returns>Result of the logical OR operation.</returns>
        /// <remarks>
        /// See https://www.codeproject.com/Tips/1264928/Throttling-Multiple-Tasks-to-Process-Requests-in-C.
        /// </remarks>
        public static bool LogicalOr(this IEnumerable<Func<bool>> source)
        {
            if (source is null || !source.Any())
            {
                return false;
            }

            foreach (var item in source)
            {
                if (item())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get exclusions of two collections.
        /// </summary>
        /// <typeparam name="T">Type of the collection element.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="target">Target collection.</param>
        /// <returns>List of exclusions.</returns>
        /// <remarks>
        /// Usage: when comparing IDs, then
        /// item with ID missing in the exclusion list should be created;
        /// item with ID present in the exclusion list should be deleted.
        /// </remarks>
        public static IList<T> GetExclusions<T>(this IEnumerable<T> source, IEnumerable<T> target)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            var i1 = new HashSet<T>(source);
            var i2 = new HashSet<T>(target);

            var leftDiff = i1.Except(i2).ToArray();
            var rightDiff = i2.Except(i1).ToArray();

            var exclusions = leftDiff.Concat(rightDiff).ToList();

            return exclusions;
        }

        /// <summary>
        /// Executes asynchronous action over all elements if a specified collection.
        /// </summary>
        /// <typeparam name="T">Type of the collection element.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="limit">Maximal number of asynchronous calls.</param>
        /// <param name="action">Action to be executed.</param>
        /// <returns>Task.</returns>
        public static Task ExecuteParallelAsync<T>(this IEnumerable<T> source, int limit, Func<T, Task> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return ExecuteParallelInternalAsync(source, limit, action);
        }

        private static async Task ExecuteParallelInternalAsync<T>(IEnumerable<T> source, int limit, Func<T, Task> action)
        {
            var allTasks = new List<Task>(); // store all tasks
            var activeTasks = new List<Task>();

            foreach (var item in source)
            {
                if (activeTasks.Count >= limit)
                {
                    var completedTask = await Task.WhenAny(activeTasks).ConfigureAwait(false);
                    activeTasks.Remove(completedTask);
                }

                var task = action(item);
                allTasks.Add(task);
                activeTasks.Add(task);
            }

            await Task.WhenAll(allTasks).ConfigureAwait(false); // Wait for all task to complete
        }
    }
}
