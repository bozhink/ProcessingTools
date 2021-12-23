// <copyright file="QueryableExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            return sortOrder switch
            {
                SortOrder.Ascending => source.OrderBy(keySelector: propertyName.ToExpressionFromPropertyName<TSource, object>()),
                SortOrder.Descending => source.OrderByDescending(keySelector: propertyName.ToExpressionFromPropertyName<TSource, object>()),
                _ => source,
            };
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
            if (source is null)
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

            return sortOrder switch
            {
                'D' => source.OrderByDescending(keySelector: propertyName.ToExpressionFromPropertyName<TSource, object>()),
                _ => source.OrderBy(keySelector: propertyName.ToExpressionFromPropertyName<TSource, object>()),
            };
        }

        /// <summary>
        /// Order by sort expression.
        /// </summary>
        /// <typeparam name="T">Type of the source items.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="sortExpression">Sort expression to be applied. It must be in the format "&lt;columnName&gt;[ DESCENDING]".</param>
        /// <param name="defaultSort">Default sort column name.</param>
        /// <returns>An <see cref="IOrderedQueryable{T}"/> whose elements are sorted according to the sort expression.</returns>
        public static IOrderedQueryable<T> OrderBySortExpression<T>(this IQueryable<T> source, string sortExpression, string defaultSort = "ID")
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            string orderMethod = nameof(Queryable.OrderBy);
            string se = string.IsNullOrEmpty(sortExpression) ? defaultSort : sortExpression;

            string[] sortKeys;

            if (se?.IndexOf(",", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                sortKeys = se.Split(',');
            }
            else
            {
                sortKeys = new[] { se };
            }

            object result = source;

            for (int i = 0; i < sortKeys.Length; i++)
            {
                string[] split = sortKeys[i].Split(' ');
                string sort = split[0];
                bool descending = split.Length > 1 && split[1].StartsWith("DESCENDING", StringComparison.InvariantCultureIgnoreCase);
                if (i == 0)
                {
                    orderMethod = descending ? nameof(Queryable.OrderByDescending) : nameof(Queryable.OrderBy);
                }
                else
                {
                    orderMethod = descending ? nameof(Queryable.ThenByDescending) : nameof(Queryable.ThenBy);
                }

                ParameterExpression arg = Expression.Parameter(typeof(T), "x");
                Expression expr = arg;
                Type type = typeof(T);

                // Split for nested properties
                string[] properties = sort.ToUpperInvariant().Split('.');

                foreach (string property in properties)
                {
                    PropertyInfo propertyInfo = type.GetProperties().FirstOrDefault(t => t.Name.ToUpperInvariant() == property);
                    expr = Expression.Property(expr, propertyInfo);
                    type = propertyInfo.PropertyType;
                }

                // Construct lambda
                Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
                LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

                var method = typeof(Queryable).GetMethods()
                    .Single(
                        m =>
                            m.Name == orderMethod &&
                            m.IsGenericMethodDefinition &&
                            m.GetGenericArguments().Length == 2 &&
                            m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type);

                result = method.Invoke(null, new object[] { result, lambda });
            }

            return (IOrderedQueryable<T>)result;
        }

        /// <summary>
        /// Applies simple filter on a sequence of values.
        /// </summary>
        /// <typeparam name="T">Type of the source items.</typeparam>
        /// <param name="source">A sequence of values to be filtered.</param>
        /// <param name="filterExpression">Filter expression to be applied. It must be of the form &lt;propertyNam&gt;=&lt;value&gt;.</param>
        /// <returns>An <see cref="IQueryable{T}"/> whose elements are filtered according to the filter expression.</returns>
        public static IQueryable<T> SimpleFilter<T>(this IQueryable<T> source, string filterExpression)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            string[] filterExpressionValues = filterExpression?.Split('=') ?? Array.Empty<string>();
            if (filterExpressionValues.Length > 1)
            {
                string columnName = filterExpressionValues[0].ToUpperInvariant();
                string value = filterExpressionValues[1];

                PropertyInfo property = typeof(T).GetProperties().FirstOrDefault(w => w.Name.ToUpperInvariant() == columnName);
                if (property != null)
                {
                    ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
                    Expression leftExpression = Expression.Property(parameter, property);
                    Expression rightExpression = Expression.Constant(value);
                    var experssion = Expression.Equal(leftExpression, rightExpression);
                    var lambda = Expression.Lambda<Func<T, bool>>(experssion, parameter);

                    var method = typeof(Queryable).GetMethods()
                        .Single(m => m.Name == nameof(Queryable.Where) && m.IsGenericMethodDefinition)
                        .MakeGenericMethod(typeof(T));

                    var result = method.Invoke(null, new object[] { source, lambda });

                    return (IQueryable<T>)result;
                }
            }

            return source;
        }
    }
}
