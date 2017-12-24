// <copyright file="DynamicExpression.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace System.Linq.Dynamic
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Dynamic Expression
    /// </summary>
    public static class DynamicExpression
    {
        /// <summary>
        /// Parse string expression.
        /// </summary>
        /// <param name="parameters">Parameters of the parsed expression.</param>
        /// <param name="resultType">Result type of the parsed expression.</param>
        /// <param name="expression">The expression as string.</param>
        /// <param name="values">Values for the expression.</param>
        /// <returns>Parsed expression.</returns>
        public static Expression Parse(ParameterExpression[] parameters, Type resultType, string expression, params object[] values)
        {
            ExpressionParser parser = new ExpressionParser(parameters, expression, values);
            return parser.Parse(resultType);
        }

        /// <summary>
        /// Parse string expression.
        /// </summary>
        /// <param name="resultType">Result type of the parsed expression.</param>
        /// <param name="expression">The expression as string.</param>
        /// <param name="values">Values for the expression.</param>
        /// <returns>Parsed expression.</returns>
        public static Expression Parse(Type resultType, string expression, params object[] values)
        {
            ExpressionParser parser = new ExpressionParser(null, expression, values);
            return parser.Parse(resultType);
        }

        /// <summary>
        /// Parse string lambda.
        /// </summary>
        /// <param name="itType">It type.</param>
        /// <param name="resultType">Result type.</param>
        /// <param name="expression">The expression as string.</param>
        /// <param name="values">Values for the expression.</param>
        /// <returns>Parsed lambda.</returns>
        public static LambdaExpression ParseLambda(Type itType, Type resultType, string expression, params object[] values)
        {
            return ParseLambda(new ParameterExpression[] { Expression.Parameter(itType, string.Empty) }, resultType, expression, values);
        }

        /// <summary>
        /// Parse string lambda.
        /// </summary>
        /// <param name="parameters">Parameters of the parsed lambda.</param>
        /// <param name="resultType">Result type of the parsed lambda.</param>
        /// <param name="expression">The expression as string.</param>
        /// <param name="values">Values for the expression.</param>
        /// <returns>Parsed lambda.</returns>
        public static LambdaExpression ParseLambda(ParameterExpression[] parameters, Type resultType, string expression, params object[] values)
        {
            ExpressionParser parser = new ExpressionParser(parameters, expression, values);
            return Expression.Lambda(parser.Parse(resultType), parameters);
        }

        /// <summary>
        /// Parse string lambda.
        /// </summary>
        /// <param name="delegateType">Delegate type.</param>
        /// <param name="parameters">Parameters of the parsed lambda.</param>
        /// <param name="resultType">Result type of the parsed lambda.</param>
        /// <param name="expression">The expression as string.</param>
        /// <param name="values">Values for the expression.</param>
        /// <returns>Parsed lambda.</returns>
        public static LambdaExpression ParseLambda(Type delegateType, ParameterExpression[] parameters, Type resultType, string expression, params object[] values)
        {
            ExpressionParser parser = new ExpressionParser(parameters, expression, values);
            return Expression.Lambda(delegateType, parser.Parse(resultType), parameters);
        }

        /// <summary>
        /// Parse string lambda.
        /// </summary>
        /// <typeparam name="T">Input type.</typeparam>
        /// <typeparam name="S">Output type.</typeparam>
        /// <param name="expression">The expression as string.</param>
        /// <param name="values">Values for the expression.</param>
        /// <returns>Parsed lambda.</returns>
        public static Expression<Func<T, S>> ParseLambda<T, S>(string expression, params object[] values)
        {
            return (Expression<Func<T, S>>)ParseLambda(typeof(T), typeof(S), expression, values);
        }

        /// <summary>
        /// Creates class with properties.
        /// </summary>
        /// <param name="properties">Properties for the resultant type.</param>
        /// <returns>Resultant type.</returns>
        public static Type CreateClass(params DynamicProperty[] properties)
        {
            return ClassFactory.Instance.GetDynamicClass(properties);
        }

        /// <summary>
        /// Creates class with properties.
        /// </summary>
        /// <param name="properties">Properties for the resultant type.</param>
        /// <returns>Resultant type.</returns>
        public static Type CreateClass(IEnumerable<DynamicProperty> properties)
        {
            return ClassFactory.Instance.GetDynamicClass(properties);
        }
    }
}
