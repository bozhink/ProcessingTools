// <copyright file="ExpressionExtensions.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq.Expressions
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// System.Linq.Expressions extensions.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// See http://stackoverflow.com/questions/27669993/creating-a-property-selector-expression-from-a-string
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <typeparam name="S">S</typeparam>
        /// <param name="propertyName">propertyName</param>
        /// <returns>Expression</returns>
        public static Expression<Func<T, S>> ToExpressionFromPropertyName<T, S>(this string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var body = Expression.PropertyOrField(parameter, propertyName);
            return Expression.Lambda<Func<T, S>>(body, parameter);
        }
    }
}
