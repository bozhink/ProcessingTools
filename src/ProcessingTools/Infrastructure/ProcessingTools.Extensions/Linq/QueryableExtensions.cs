// <copyright file="QueryableExtensions.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq
{
    using System;
    using System.Linq;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Extensions.Linq.Expressions;

    /// <summary>
    /// <see cref="IQueryable{T}"/> extensions.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Sorts the elements of a sequence according to a property name.
        /// </summary>
        /// <typeparam name="TSource">Type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="propertyName">Property name to sort.</param>
        /// <param name="sortOrder"><see cref="SortOrder"/> value.</param>
        /// <returns>Sorted sequence.</returns>
        public static IQueryable<TSource> OrderByName<TSource>(this IQueryable<TSource> source, string propertyName, SortOrder sortOrder)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            switch (sortOrder)
            {
                case SortOrder.Ascending:
                    return source.OrderBy(keySelector: propertyName.ToExpressionFromPropertyName<TSource, object>());

                case SortOrder.Descending:
                    return source.OrderByDescending(keySelector: propertyName.ToExpressionFromPropertyName<TSource, object>());

                default:
                    return source;
            }
        }

        /// <summary>
        /// Sorts the elements of a sequence according to a property name.
        /// </summary>
        /// <typeparam name="TSource">Type of the elements of source.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="name">Property name to sort and sort direction. It have to be of type &lt;Name[ (A|D)]&gt;.</param>
        /// <returns>Sorted sequence.</returns>
        public static IQueryable<TSource> OrderByName<TSource>(this IQueryable<TSource> source, string name)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            string[] nameParts = name.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (nameParts.Length < 1)
            {
                return source;
            }

            string propertyName = nameParts[0];
            char sortOrder = 'A';
            if (nameParts.Length > 1)
            {
                sortOrder = nameParts[1].ToUpperInvariant()[1];
            }

            switch (sortOrder)
            {
                case 'D':
                    return source.OrderByDescending(keySelector: propertyName.ToExpressionFromPropertyName<TSource, object>());

                default:
                    return source.OrderBy(keySelector: propertyName.ToExpressionFromPropertyName<TSource, object>());
            }
        }
    }
}
