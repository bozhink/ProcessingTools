﻿// <copyright file="ExpressionExtensions.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// System.Linq.Expressions extensions.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Map property name to expression.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <typeparam name="S">S</typeparam>
        /// <param name="propertyName">Property name.</param>
        /// <returns>Expression</returns>
        /// <remarks>
        /// See http://stackoverflow.com/questions/27669993/creating-a-property-selector-expression-from-a-string
        /// </remarks>
        public static Expression<Func<T, S>> ToExpressionFromPropertyName<T, S>(this string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var body = Expression.PropertyOrField(parameter, propertyName);
            return Expression.Lambda<Func<T, S>>(body, parameter);
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
            // build parameter map (from parameters of second to parameters of first)
            var map = expression1.Parameters
                .Select((f, i) => new
                {
                    First = f,
                    Second = expression2.Parameters[i]
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
        /// See http://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool
        /// </remarks>
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
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
        /// See http://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool
        /// </remarks>
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            var parameter = expression.Parameters[0];
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), parameter);
        }

        /// <summary>
        /// Returns concatenated expressions with OrElse operator.
        /// </summary>
        /// <typeparam name="T">Input type</typeparam>
        /// <param name="expression1">Left expression to be concatenated.</param>
        /// <param name="expression2">Right expression to be concatenated.</param>
        /// <returns>Concatenated expressions with OrElse operator.</returns>
        /// <remarks>
        /// See http://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool
        /// </remarks>
        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
        {
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
        /// <typeparam name="S">Input type.</typeparam>
        /// <typeparam name="T">Output type.</typeparam>
        /// <param name="lambda">Lambda expression.</param>
        /// <returns>Re-mapped expression.</returns>
        public static Expression<Func<S, T>> ToExpression<S, T>(this LambdaExpression lambda)
        {
            ParameterExpression lambdaParameter = lambda.Parameters.Single();
            ParameterExpression parameter = Expression.Parameter(typeof(S), lambdaParameter.Name);

            var symbols = new Dictionary<string, object>
            {
                { parameter.ToString(), parameter }
            };

            var expression = lambda.Body.ToString();
            var body = System.Linq.Dynamic.DynamicExpressionParser.Parse(typeof(T), expression, symbols);

            return Expression.Lambda<Func<S, T>>(body, parameter);
        }

        /// <summary>
        /// Re-map expression to new expression.
        /// </summary>
        /// <typeparam name="S">S</typeparam>
        /// <typeparam name="B">B</typeparam>
        /// <typeparam name="T">T</typeparam>
        /// <param name="expression">The expression to be re-mapped.</param>
        /// <returns>Re-mapped expression.</returns>
        public static Expression<Func<B, T>> ToExpression<S, B, T>(this Expression<Func<S, T>> expression)
        {
            var visitor = new GenericExpressionVisitor<S, B>();

            var query = (Expression<Func<B, T>>)visitor.Visit(expression);

            return query;
        }

        /// <summary>
        /// Convert expression to new expression.
        /// </summary>
        /// <typeparam name="S">S</typeparam>
        /// <typeparam name="B">B</typeparam>
        /// <typeparam name="T">T</typeparam>
        /// <param name="expression">Expression to be converted.</param>
        /// <returns>Converted expression.</returns>
        /// <remarks>
        /// See http://stackoverflow.com/questions/14007101/how-can-i-convert-a-lambda-expression-between-different-but-compatible-models
        /// </remarks>
        public static Expression<Func<B, T>> Convert<S, B, T>(this Expression<Func<S, T>> expression)
        {
            return Convert<Func<S, T>, Func<B, T>>(expression);
        }

        // See http://stackoverflow.com/questions/14007101/how-can-i-convert-a-lambda-expression-between-different-but-compatible-models
        private static Expression<B> Convert<S, B>(Expression<S> expression)
            where S : class
            where B : class
        {
            // figure out which types are different in the function-signature
            var mapFromTypes = expression.Type.GetGenericArguments();
            var mapToTypes = typeof(B).GetGenericArguments();
            if (mapFromTypes.Length != mapToTypes.Length)
            {
                throw new NotSupportedException("Incompatible lambda function-type signatures");
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
            return Expression.Lambda<B>(body, newParameters);
        }
    }
}
