namespace ProcessingTools.Extensions.Linq.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public static class ExpressionExtensions
    {
        public static Expression<Func<T, S>> ToExpression<T, S>(this LambdaExpression lambda)
        {
            ParameterExpression lambdaParameter = lambda.Parameters.Single();
            ParameterExpression parameter = Expression.Parameter(typeof(T), lambdaParameter.Name);

            var symbols = new Dictionary<string, object>();
            symbols.Add(parameter.ToString(), parameter);

            var expression = lambda.Body.ToString();
            var body = System.Linq.Dynamic.DynamicExpression.Parse(typeof(S), expression, symbols);

            return Expression.Lambda<Func<T, S>>(body, parameter);
        }
    }
}
