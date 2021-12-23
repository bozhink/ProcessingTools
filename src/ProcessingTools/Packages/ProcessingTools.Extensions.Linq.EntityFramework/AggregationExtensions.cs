// <copyright file="AggregationExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq.EntityFramework
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Aggregation extensions.
    /// </summary>
    /// <remarks>
    /// See https://stackoverflow.com/questions/2165605/whats-the-neatest-way-to-achieve-minordefault-in-linq.
    /// </remarks>
    public static class AggregationExtensions
    {
        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="defaultValue">Default value.</param>
        /// <returns>Minimal or default value.</returns>
        public static Task<TSource> MinOrDefaultAsync<TSource>(this IQueryable<TSource> source, TSource defaultValue)
        {
            return source.DefaultIfEmpty(defaultValue).MinAsync();
        }

        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <returns>Minimal or default value.</returns>
        public static Task<TSource> MinOrDefaultAsync<TSource>(this IQueryable<TSource> source)
        {
            return source.DefaultIfEmpty().MinAsync();
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
        public static Task<TResult> MinOrDefaultAsync<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, TSource defaultValue)
        {
            return source.DefaultIfEmpty(defaultValue).MinAsync(selector);
        }

        /// <summary>
        /// Get minimal value or default.
        /// </summary>
        /// <typeparam name="TSource">Type of source collection.</typeparam>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="source">Source collection.</param>
        /// <param name="selector">Filter selector.</param>
        /// <returns>Minimal or default value.</returns>
        public static Task<TResult> MinOrDefaultAsync<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
        {
            return source.DefaultIfEmpty().MinAsync(selector);
        }
    }
}
