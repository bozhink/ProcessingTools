// <copyright file="DynamicQueryable.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq.Dynamic
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
#pragma warning disable SA1600,CS1591

        public static IQueryable<T> Where<T>(this IQueryable<T> source, string predicate, params object[] values)
        {
            return (IQueryable<T>)Where((IQueryable)source, predicate, values);
        }

        public static IQueryable Where(this IQueryable source, string predicate, params object[] values)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate == null)
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

        public static IQueryable Select(this IQueryable source, string selector, params object[] values)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector == null)
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

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string ordering, params object[] values)
        {
            return (IQueryable<T>)OrderBy((IQueryable)source, ordering, values);
        }

        public static IQueryable OrderBy(this IQueryable source, string ordering, params object[] values)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (ordering == null)
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

        public static IQueryable Take(this IQueryable source, int count)
        {
            if (source == null)
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

        public static IQueryable Skip(this IQueryable source, int count)
        {
            if (source == null)
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

        public static IQueryable GroupBy(this IQueryable source, string keySelector, string elementSelector, params object[] values)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (elementSelector == null)
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

        public static bool Any(this IQueryable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return (bool)source.Provider.Execute(
                Expression.Call(
                    typeof(Queryable),
                    nameof(Queryable.Any),
                    new[] { source.ElementType },
                    source.Expression));
        }

        public static int Count(this IQueryable source)
        {
            if (source == null)
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

        public static IQueryable Distinct(this IQueryable source)
        {
            if (source == null)
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
        /// Dynamically runs an aggregate function on the IQueryable.
        /// </summary>
        /// <param name="source">The IQueryable data source.</param>
        /// <param name="function">The name of the function to run. Can be Sum, Average, Min, Max.</param>
        /// <param name="member">The name of the property to aggregate over.</param>
        /// <returns>The value of the aggregate function run over the specified property.</returns>
        public static object Aggregate(this IQueryable source, string function, string member)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (member == null)
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

        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, string predicate, params object[] values)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().Where(predicate, values);
        }

        public static IEnumerable Where(this IEnumerable source, string predicate, params object[] values)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().Where(predicate, values);
        }

        public static IEnumerable Select(this IEnumerable source, string selector, params object[] values)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().Select(selector, values);
        }

        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source, string ordering, params object[] values)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().OrderBy(ordering, values);
        }

        public static IEnumerable OrderBy(this IEnumerable source, string ordering, params object[] values)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().OrderBy(ordering, values);
        }

        public static IEnumerable Take(this IEnumerable source, int count)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().Take(count);
        }

        public static IEnumerable Skip(this IEnumerable source, int count)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().Skip(count);
        }

        public static IEnumerable GroupBy(this IEnumerable source, string keySelector, string elementSelector, params object[] values)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().GroupBy(keySelector, elementSelector, values);
        }

        public static bool Any(this IEnumerable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().Any();
        }

        public static int Count(this IEnumerable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().Count();
        }

        public static IEnumerable Distinct(this IEnumerable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.AsQueryable().Distinct();
        }

#pragma warning restore SA1600,CS1591
    }
}
