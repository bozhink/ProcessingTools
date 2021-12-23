// <copyright file="ExpressionExtensions.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using ProcessingTools.Extensions.Dynamic;

    /// <summary>
    /// System.Linq.Expressions extensions.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Map property name to expression.
        /// </summary>
        /// <typeparam name="T1">T1.</typeparam>
        /// <typeparam name="T2">T2.</typeparam>
        /// <param name="propertyName">Property name.</param>
        /// <returns>Expression.</returns>
        /// <remarks>
        /// See http://stackoverflow.com/questions/27669993/creating-a-property-selector-expression-from-a-string.
        /// </remarks>
        public static Expression<Func<T1, T2>> ToExpressionFromPropertyName<T1, T2>(this string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            var parameter = Expression.Parameter(typeof(T1));
            var body = Expression.PropertyOrField(parameter, propertyName);
            return Expression.Lambda<Func<T1, T2>>(body, parameter);
        }

        /// <summary>
        /// Composes two expressions by specified merge expression.
        /// </summary>
        /// <typeparam name="T">Type of expression.</typeparam>
        /// <param name="expression1">Left expression in composition.</param>
        /// <param name="expression2">Right expression in composition.</param>
        /// <param name="merge">Merge expression.</param>
        /// <returns>Expression composition.</returns>
        public static Expression<T> Compose<T>(this Expression<T> expression1, Expression<T> expression2, Func<Expression, Expression, Expression> merge)
        {
            if (expression1 is null)
            {
                throw new ArgumentNullException(nameof(expression1));
            }

            if (expression2 is null)
            {
                throw new ArgumentNullException(nameof(expression2));
            }

            if (merge is null)
            {
                throw new ArgumentNullException(nameof(merge));
            }

            // build parameter map (from parameters of second to parameters of first)
            var map = expression1.Parameters
                .Select((f, i) => new
                {
                    First = f,
                    Second = expression2.Parameters[i],
                })
                .ToDictionary(p => p.Second, p => p.First);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinderExpressionVisitor.ReplaceParameters(map, expression2.Body);

            // apply composition of lambda expression bodies to parameters from the first expression
            return Expression.Lambda<T>(merge(expression1.Body, secondBody), expression1.Parameters);
        }

        /// <summary>
        /// Returns composed expressions with And operator.
        /// </summary>
        /// <typeparam name="T">Input type.</typeparam>
        /// <param name="expression1">Left expression in composition.</param>
        /// <param name="expression2">Right expression in composition.</param>
        /// <returns>Composed expressions with And operator.</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            if (expression1 is null)
            {
                throw new ArgumentNullException(nameof(expression1));
            }

            if (expression2 is null)
            {
                throw new ArgumentNullException(nameof(expression2));
            }

            return expression1.Compose(expression2, Expression.And);
        }

        /// <summary>
        /// Returns composed expressions with Or operator.
        /// </summary>
        /// <typeparam name="T">Input type.</typeparam>
        /// <param name="expression1">Left expression in composition.</param>
        /// <param name="expression2">Right expression in composition.</param>
        /// <returns>Composed expressions with Or operator.</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            if (expression1 is null)
            {
                throw new ArgumentNullException(nameof(expression1));
            }

            if (expression2 is null)
            {
                throw new ArgumentNullException(nameof(expression2));
            }

            return expression1.Compose(expression2, Expression.Or);
        }

        /// <summary>
        /// Returns concatenated expressions with AndAlso operator.
        /// </summary>
        /// <typeparam name="T">Input type.</typeparam>
        /// <param name="expression1">Left expression to be concatenated.</param>
        /// <param name="expression2">Right expression to be concatenated.</param>
        /// <returns>Concatenated expressions with AndAlso operator.</returns>
        /// <remarks>
        /// See [http://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool].
        /// </remarks>
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            if (expression1 is null)
            {
                throw new ArgumentNullException(nameof(expression1));
            }

            if (expression2 is null)
            {
                throw new ArgumentNullException(nameof(expression2));
            }

            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expression1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expression1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expression2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expression2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }

        /// <summary>
        /// Returns Not operator over expression.
        /// </summary>
        /// <typeparam name="T">Input type.</typeparam>
        /// <param name="expression">Input expression.</param>
        /// <returns>Not operator over expression.</returns>
        /// <remarks>
        /// See [http://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool].
        /// </remarks>
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var parameter = expression.Parameters[0];
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), parameter);
        }

        /// <summary>
        /// Returns concatenated expressions with OrElse operator.
        /// </summary>
        /// <typeparam name="T">Input type.</typeparam>
        /// <param name="expression1">Left expression to be concatenated.</param>
        /// <param name="expression2">Right expression to be concatenated.</param>
        /// <returns>Concatenated expressions with OrElse operator.</returns>
        /// <remarks>
        /// See [http://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool].
        /// </remarks>
        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
            if (expression1 is null)
            {
                throw new ArgumentNullException(nameof(expression1));
            }

            if (expression2 is null)
            {
                throw new ArgumentNullException(nameof(expression2));
            }

            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expression1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expression1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expression2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expression2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), parameter);
        }

        /// <summary>
        /// Re-map expression to new expression.
        /// </summary>
        /// <typeparam name="T1">Input type.</typeparam>
        /// <typeparam name="T2">Output type.</typeparam>
        /// <param name="lambda">Lambda expression.</param>
        /// <returns>Re-mapped expression.</returns>
        public static Expression<Func<T1, T2>> ToExpression<T1, T2>(this LambdaExpression lambda)
        {
            if (lambda is null)
            {
                throw new ArgumentNullException(nameof(lambda));
            }

            ParameterExpression lambdaParameter = lambda.Parameters.Single();
            ParameterExpression parameter = Expression.Parameter(typeof(T1), lambdaParameter.Name);

            var symbols = new Dictionary<string, object>
            {
                { parameter.ToString(), parameter },
            };

            var expression = lambda.Body.ToString();
            var body = DynamicExpressionParser.ParseLambda(new[] { parameter }, typeof(T2), expression, symbols);

            return Expression.Lambda<Func<T1, T2>>(body, parameter);
        }

        /// <summary>
        /// Re-map expression to new expression.
        /// </summary>
        /// <typeparam name="T1">T1.</typeparam>
        /// <typeparam name="T2">T2.</typeparam>
        /// <typeparam name="T3">T3.</typeparam>
        /// <param name="expression">The expression to be re-mapped.</param>
        /// <returns>Re-mapped expression.</returns>
        public static Expression<Func<T2, T3>> ToExpression<T1, T2, T3>(this Expression<Func<T1, T3>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var visitor = new GenericExpressionVisitor<T1, T2>();

            var query = (Expression<Func<T2, T3>>)visitor.Visit(expression);

            return query;
        }

        /// <summary>
        /// Convert expression to new expression.
        /// </summary>
        /// <typeparam name="T1">T1.</typeparam>
        /// <typeparam name="T2">T2.</typeparam>
        /// <typeparam name="T3">T3.</typeparam>
        /// <param name="expression">Expression to be converted.</param>
        /// <returns>Converted expression.</returns>
        /// <remarks>
        /// See [http://stackoverflow.com/questions/14007101/how-can-i-convert-a-lambda-expression-between-different-but-compatible-models].
        /// </remarks>
        public static Expression<Func<T2, T3>> Convert<T1, T2, T3>(this Expression<Func<T1, T3>> expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return Convert<Func<T1, T3>, Func<T2, T3>>(expression);
        }

        // See http://stackoverflow.com/questions/14007101/how-can-i-convert-a-lambda-expression-between-different-but-compatible-models
        private static Expression<T2> Convert<T1, T2>(Expression<T1> expression)
            where T1 : class
            where T2 : class
        {
            // figure out which types are different in the function-signature
            var mapFromTypes = expression.Type.GetGenericArguments();
            var mapToTypes = typeof(T2).GetGenericArguments();
            if (mapFromTypes.Length != mapToTypes.Length)
            {
                throw new InvalidOperationException();
            }

            var typeMap = new Dictionary<Type, Type>();
            for (int i = 0; i < mapFromTypes.Length; i++)
            {
                if (mapFromTypes[i] != mapToTypes[i])
                {
                    typeMap[mapFromTypes[i]] = mapToTypes[i];
                }
            }

            // re-map all parameters that involve different types
            var parameterMap = new Dictionary<Expression, Expression>();
            var newParameters = new ParameterExpression[expression.Parameters.Count];
            for (int i = 0; i < newParameters.Length; i++)
            {
                var parameter = expression.Parameters[i];
                if (parameter != null)
                {
                    if (typeMap.TryGetValue(parameter.Type, out Type newType))
                    {
                        newParameters[i] = Expression.Parameter(newType, parameter.Name);
                        parameterMap[parameter] = newParameters[i];
                    }
                    else
                    {
                        newParameters[i] = parameter;
                    }
                }
            }

            // rebuild the lambda
            var body = new TypeConversionVisitor(parameterMap).Visit(expression.Body);
            return Expression.Lambda<T2>(body, newParameters);
        }
    }
}
