// <copyright file="DynamicQueryable.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Dynamic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Microsoft provided class. It allows dynamic string based querying.
    /// Very handy when, at compile time, you don't know the type of queries that will be generated.
    /// </summary>
    public static class DynamicQueryable
    {
        /// <summary>
        /// Dynamically runs an aggregate function on the IQueryable.
        /// </summary>
        /// <param name="source">The IQueryable data source.</param>
        /// <param name="function">The name of the function to run. Can be Sum, Average, Min, Max.</param>
        /// <param name="member">The name of the property to aggregate over.</param>
        /// <returns>The value of the aggregate function run over the specified property.</returns>
        public static object Aggregate(this IQueryable source, string function, string member)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            // Properties
            PropertyInfo property = source.ElementType.GetProperty(member);
            ParameterExpression parameter = Expression.Parameter(source.ElementType, "s");
            Expression selector = Expression.Lambda(Expression.MakeMemberAccess(parameter, property), parameter);

            //// We've tried to find an expression of the type Expression<Func<TSource, TAcc>>,
            //// which is expressed as ( (TSource s) => s.Price );
            var methods = typeof(Queryable).GetMethods();

            // Method
            MethodInfo aggregateMethod = methods.SingleOrDefault(
                m =>
                    m.Name == function &&
                    m.ReturnType == property.PropertyType &&
                    m.IsGenericMethod);

            if (aggregateMethod != null)
            {
                // Sum, Average
                return source.Provider.Execute(
                    Expression.Call(
                        null,
                        aggregateMethod.MakeGenericMethod(new[] { source.ElementType }),
                        new[] { source.Expression, Expression.Quote(selector) }));
            }
            else
            {
                // Min, Max
                aggregateMethod = methods.SingleOrDefault(
                    m =>
                        m.Name == function &&
                        m.GetGenericArguments().Length == 2 &&
                        m.IsGenericMethod);

                return source.Provider.Execute(
                    Expression.Call(
                        null,
                        aggregateMethod.MakeGenericMethod(new[] { source.ElementType, property.PropertyType }),
                        new[] { source.Expression, Expression.Quote(selector) }));
            }
        }

        /// <summary>
        /// Determines whether a sequence contains any elements.
        /// </summary>
        /// <param name="source">A sequence to check for being empty.</param>
        /// <returns>True if the source sequence contains any elements; otherwise, False.</returns>
        public static bool Any(this IQueryable source)
        {
            if (source is null)
            {
                return false;
            }

            return (bool)source.Provider.Execute(
                Expression.Call(
                    typeof(Queryable),
                    nameof(Queryable.Any),
                    new[] { source.ElementType },
                    source.Expression));
        }

        /// <summary>
        /// Determines whether a sequence contains any elements.
        /// </summary>
        /// <param name="source">A sequence to check for being empty.</param>
        /// <returns>True if the source sequence contains any elements; otherwise, False.</returns>
        public static bool Any(this IEnumerable source)
        {
            if (source is null)
            {
                return false;
            }

            return source.AsQueryable().Any();
        }

        /// <summary>
        /// Returns the number of elements in a sequence.
        /// </summary>
        /// <param name="source">The <see cref="IQueryable"/> that contains the elements to be counted.</param>
        /// <returns>The number of elements in the input sequence.</returns>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        public static int Count(this IQueryable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return (int)source.Provider.Execute(
                Expression.Call(
                    typeof(Queryable),
                    nameof(Queryable.Count),
                    new[] { source.ElementType },
                    source.Expression));
        }

        /// <summary>
        /// Returns the number of elements in a sequence.
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable"/> that contains the elements to be counted.</param>
        /// <returns>The number of elements in the input sequence.</returns>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        public static int Count(this IEnumerable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().Count();
        }

        /// <summary>
        /// Returns distinct elements from a sequence by using the default equality comparer to compare values.
        /// </summary>
        /// <param name="source">The <see cref="IQueryable"/> to remove duplicates from.</param>
        /// <returns>An <see cref="IQueryable"/> that contains distinct elements from source.</returns>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        public static IQueryable Distinct(this IQueryable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    nameof(Queryable.Distinct),
                    new[] { source.ElementType },
                    source.Expression));
        }

        /// <summary>
        /// Returns distinct elements from a sequence by using the default equality comparer to compare values.
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable"/> to remove duplicates from.</param>
        /// <returns>An <see cref="IEnumerable"/> that contains distinct elements from source.</returns>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        public static IEnumerable Distinct(this IEnumerable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().Distinct();
        }

        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function.
        /// </summary>
        /// <param name="source">An <see cref="IQueryable"/> whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="elementSelector">A function to map each source element to an element in an <see cref="IQueryable"/>.</param>
        /// <param name="values">Values for selection.</param>
        /// <returns>An <see cref="IQueryable"/> of the grouping.</returns>
        /// <exception cref="ArgumentNullException">Source or keySelector or elementSelector is null.</exception>
        public static IQueryable GroupBy(this IQueryable source, string keySelector, string elementSelector, params object[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (elementSelector is null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }

            LambdaExpression keyLambda = DynamicExpressionParser.ParseLambda(source.ElementType, null, keySelector, values);
            LambdaExpression elementLambda = DynamicExpressionParser.ParseLambda(source.ElementType, null, elementSelector, values);
            return source.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    nameof(Queryable.GroupBy),
                    new[] { source.ElementType, keyLambda.Body.Type, elementLambda.Body.Type },
                    source.Expression,
                    Expression.Quote(keyLambda),
                    Expression.Quote(elementLambda)));
        }

        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function.
        /// </summary>
        /// <param name="source">An <see cref="IEnumerable"/> whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="elementSelector">A function to map each source element to an element in an <see cref="IEnumerable"/>.</param>
        /// <param name="values">Values for selection.</param>
        /// <returns>An <see cref="IEnumerable"/> of the grouping.</returns>
        /// <exception cref="ArgumentNullException">Source or keySelector or elementSelector is null.</exception>
        public static IEnumerable GroupBy(this IEnumerable source, string keySelector, string elementSelector, params object[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().GroupBy(keySelector, elementSelector, values);
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending or descending order according to a key.
        /// </summary>
        /// <typeparam name="T">Type of the <see cref="IQueryable"/> element.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="ordering">Ordering expression.</param>
        /// <param name="values">Values for the expression.</param>
        /// <returns>An ordered <see cref="IQueryable"/> whose elements are sorted according to an expression.</returns>
        /// <exception cref="ArgumentNullException">Source or ordering is null.</exception>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering, params object[] values)
        {
            return (IQueryable<T>)OrderBy((IQueryable)source, ordering, values);
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending or descending order according to a key.
        /// </summary>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="ordering">Ordering expression.</param>
        /// <param name="values">Values for the expression.</param>
        /// <returns>An ordered <see cref="IQueryable"/> whose elements are sorted according to an expression.</returns>
        /// <exception cref="ArgumentNullException">Source or ordering is null.</exception>
        public static IQueryable OrderBy(this IQueryable source, string ordering, params object[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (ordering is null)
            {
                throw new ArgumentNullException(nameof(ordering));
            }

            ParameterExpression[] parameters = new[] { Expression.Parameter(source.ElementType, string.Empty) };
            ExpressionParser parser = new ExpressionParser(parameters, ordering, values);
            IEnumerable<DynamicOrdering> orderings = parser.ParseOrdering();
            Expression queryExpression = source.Expression;
            string methodAsc = nameof(Queryable.OrderBy);
            string methodDesc = nameof(Queryable.OrderByDescending);

            foreach (var o in orderings)
            {
                queryExpression = Expression.Call(
                    typeof(Queryable),
                    o.Ascending ? methodAsc : methodDesc,
                    new[] { source.ElementType, o.Selector.Type },
                    queryExpression,
                    Expression.Quote(Expression.Lambda(o.Selector, parameters)));

                methodAsc = nameof(Queryable.ThenBy);
                methodDesc = nameof(Queryable.ThenByDescending);
            }

            return source.Provider.CreateQuery(queryExpression);
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending or descending order according to a key.
        /// </summary>
        /// <typeparam name="T">Type of the <see cref="IEnumerable"/> element.</typeparam>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="ordering">Ordering expression.</param>
        /// <param name="values">Values for the expression.</param>
        /// <returns>An ordered <see cref="IEnumerable"/> whose elements are sorted according to an expression.</returns>
        /// <exception cref="ArgumentNullException">Source or ordering is null.</exception>
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source, string ordering, params object[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().OrderBy(ordering, values);
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending or descending order according to a key.
        /// </summary>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="ordering">Ordering expression.</param>
        /// <param name="values">Values for the expression.</param>
        /// <returns>An ordered <see cref="IEnumerable"/> whose elements are sorted according to an expression.</returns>
        /// <exception cref="ArgumentNullException">Source or ordering is null.</exception>
        public static IEnumerable OrderBy(this IEnumerable source, string ordering, params object[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().OrderBy(ordering, values);
        }

        /// <summary>
        /// Projects each element of a sequence into a new form by incorporating the element's index.
        /// </summary>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="selector">A projection expression to apply to each element.</param>
        /// <param name="values">Values for the projection expression.</param>
        /// <returns>An <see cref="IQueryable"/> whose elements are the result of invoking a projection expression on each element of source.</returns>
        /// <exception cref="ArgumentNullException">Source or selector is null.</exception>
        public static IQueryable Select(this IQueryable source, string selector, params object[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            LambdaExpression lambda = DynamicExpressionParser.ParseLambda(source.ElementType, null, selector, values);

            return source.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    nameof(Queryable.Select),
                    new[] { source.ElementType, lambda.Body.Type },
                    source.Expression,
                    Expression.Quote(lambda)));
        }

        /// <summary>
        /// Projects each element of a sequence into a new form by incorporating the element's index.
        /// </summary>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="selector">A projection expression to apply to each element.</param>
        /// <param name="values">Values for the projection expression.</param>
        /// <returns>An <see cref="IEnumerable"/> whose elements are the result of invoking a projection expression on each element of source.</returns>
        /// <exception cref="ArgumentNullException">Source or selector is null.</exception>
        public static IEnumerable Select(this IEnumerable source, string selector, params object[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().Select(selector, values);
        }

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <param name="source">An <see cref="IQueryable"/> to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements.</param>
        /// <returns>An <see cref="IQueryable"/> that contains elements that occur after the specified index in the input sequence.</returns>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        public static IQueryable Skip(this IQueryable source, int count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Expression expression = Expression.Call(
                typeof(Queryable),
                nameof(Queryable.Skip),
                new[] { source.ElementType },
                source.Expression,
                Expression.Constant(count));

            return source.Provider.CreateQuery(expression);
        }

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <param name="source">An <see cref="IEnumerable"/> to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements.</param>
        /// <returns>An <see cref="IEnumerable"/> that contains elements that occur after the specified index in the input sequence.</returns>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        public static IEnumerable Skip(this IEnumerable source, int count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().Skip(count);
        }

        /// <summary>
        /// Returns a specified number of contiguous elements from the start of a sequence.
        /// </summary>
        /// <param name="source">The sequence to return elements from.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <returns>An <see cref="IQueryable"/> that contains the specified number of elements from the start of source.</returns>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        public static IQueryable Take(this IQueryable source, int count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Expression expression = Expression.Call(
                typeof(Queryable),
                nameof(Queryable.Take),
                new[] { source.ElementType },
                source.Expression,
                Expression.Constant(count));

            return source.Provider.CreateQuery(expression);
        }

        /// <summary>
        /// Returns a specified number of contiguous elements from the start of a sequence.
        /// </summary>
        /// <param name="source">The sequence to return elements from.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <returns>An <see cref="IEnumerable"/> that contains the specified number of elements from the start of source.</returns>
        /// <exception cref="ArgumentNullException">Source is null.</exception>
        public static IEnumerable Take(this IEnumerable source, int count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().Take(count);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IQueryable{T}"/> to filter.</param>
        /// <param name="predicate">An expression to test each element for a condition.</param>
        /// <param name="values">Values for the predicate.</param>
        /// <returns>An <see cref="IQueryable{T}"/> that contains elements from the input sequence that satisfy the condition specified by predicate.</returns>
        /// <exception cref="ArgumentNullException">Source or predicate is null.</exception>
        public static IQueryable<T> Where<T>(this IQueryable<T> source, string predicate, params object[] values)
        {
            return (IQueryable<T>)Where((IQueryable)source, predicate, values);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="source">An <see cref="IQueryable"/> to filter.</param>
        /// <param name="predicate">An expression to test each element for a condition.</param>
        /// <param name="values">Values for the predicate.</param>
        /// <returns>An <see cref="IQueryable"/> that contains elements from the input sequence that satisfy the condition specified by predicate.</returns>
        /// <exception cref="ArgumentNullException">Source or predicate is null.</exception>
        public static IQueryable Where(this IQueryable source, string predicate, params object[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            LambdaExpression lambda = DynamicExpressionParser.ParseLambda(source.ElementType, typeof(bool), predicate, values);

            return source.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    nameof(Queryable.Where),
                    new[] { source.ElementType },
                    source.Expression,
                    Expression.Quote(lambda)));
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to filter.</param>
        /// <param name="predicate">An expression to test each element for a condition.</param>
        /// <param name="values">Values for the predicate.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input sequence that satisfy the condition specified by predicate.</returns>
        /// <exception cref="ArgumentNullException">Source or predicate is null.</exception>
        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, string predicate, params object[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().Where(predicate, values);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="source">An <see cref="IEnumerable"/> to filter.</param>
        /// <param name="predicate">An expression to test each element for a condition.</param>
        /// <param name="values">Values for the predicate.</param>
        /// <returns>An <see cref="IEnumerable"/> that contains elements from the input sequence that satisfy the condition specified by predicate.</returns>
        /// <exception cref="ArgumentNullException">Source or predicate is null.</exception>
        public static IEnumerable Where(this IEnumerable source, string predicate, params object[] values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().Where(predicate, values);
        }
    }
}
