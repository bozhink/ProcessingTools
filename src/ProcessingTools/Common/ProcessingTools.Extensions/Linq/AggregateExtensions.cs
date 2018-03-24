// <copyright file="AggregateExtensions.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Aggregate extensions.
    /// </summary>
    /// <remarks>
    /// See https://stackoverflow.com/questions/2165605/whats-the-neatest-way-to-achieve-minordefault-in-linq
    /// </remarks>
    public static class AggregateExtensions
    {
        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <returns>Minimal or default value.</returns>
        public static TSource MinOrDefault<TSource>(this IQueryable<TSource> source)
        {
            return source.DefaultIfEmpty().Min();
        }

        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Minimal or default value.</returns>
        public static TSource MinOrDefault<TSource>(this IQueryable<TSource> source, TSource defaultValue)
        {
            return source.DefaultIfEmpty(defaultValue).Min();
        }

        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection.</typeparam>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="selector">Filter selector.</param>
        /// <returns>Minimal or default value.</returns>
        public static TResult MinOrDefault<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
        {
            return source.DefaultIfEmpty().Min(selector);
        }

        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection.</typeparam>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="selector">Filter selector.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Minimal or default value.</returns>
        public static TResult MinOrDefault<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, TSource defaultValue)
        {
            return source.DefaultIfEmpty(defaultValue).Min(selector);
        }

        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Minimal or default value.</returns>
        public static TSource? MinOrDefault<TSource>(this IEnumerable<TSource?> source, TSource? defaultValue)
            where TSource : struct
        {
            return source.Min() ?? defaultValue;
        }

        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <returns>Minimal or default value.</returns>
        public static TSource? MinOrDefault<TSource>(this IEnumerable<TSource?> source)
            where TSource : struct
        {
            return source.Min();
        }

        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection.</typeparam>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="selector">Filter selector.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Minimal or default value.</returns>
        public static TResult? Min<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult?> selector, TResult? defaultValue)
            where TResult : struct
        {
            return source.Min(selector) ?? defaultValue;
        }

        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection.</typeparam>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="selector">Filter selector.</param>
        /// <returns>Minimal or default value.</returns>
        public static TResult? Min<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult?> selector)
            where TResult : struct
        {
            return source.Min(selector);
        }

        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Minimal or default value.</returns>
        public static TSource MinOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
        {
            if (default(TSource) == null)
            {
                var result = source.Min();
                return result == null ? defaultValue : result;
            }
            else
            {
                var comparer = Comparer<TSource>.Default;
                using (var en = source.GetEnumerator())
                    if (en.MoveNext())
                    {
                        var currentMin = en.Current;
                        while (en.MoveNext())
                        {
                            var current = en.Current;
                            if (comparer.Compare(current, currentMin) < 0)
                            {
                                currentMin = current;
                            }
                        }

                        return currentMin;
                    }
            }

            return defaultValue;
        }

        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <returns>Minimal or default value.</returns>
        public static TSource MinOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            var defaultValue = default(TSource);
            return defaultValue == null ? source.Min() : source.MinOrDefault(defaultValue);
        }

        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection.</typeparam>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="selector">Filter selector.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Minimal or default value.</returns>
        public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, TResult defaultValue)
        {
            return source.Select(selector).MinOrDefault(defaultValue);
        }

        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection.</typeparam>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="selector">Filter selector.</param>
        /// <returns>Minimal or default value.</returns>
        public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.Select(selector).MinOrDefault();
        }

        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <param name="source">Source collection.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Minimal or default value.</returns>
        public static int MinOrDefault(this IEnumerable<int> source, int defaultValue)
        {
            using (var enumerator = source.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    var currentMin = enumerator.Current;
                    while (enumerator.MoveNext())
                    {
                        var current = enumerator.Current;
                        if (current < currentMin)
                        {
                            currentMin = current;
                        }
                    }

                    return currentMin;
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <param name="source">Source collection.</param>
        /// <returns>Minimal or default value.</returns>
        public static int MinOrDefault(this IEnumerable<int> source)
        {
            return source.MinOrDefault(0);
        }

        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="selector">Filter selector.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Minimal or default value.</returns>
        public static int MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector, int defaultValue)
        {
            return source.Select(selector).MinOrDefault(defaultValue);
        }

        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="selector">Filter selector.</param>
        /// <returns>Minimal or default value.</returns>
        public static int MinOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            return source.Select(selector).MinOrDefault();
        }
    }
}
