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

            var symbols = new Dictionary<string, object>
            {
                { parameter.ToString(), parameter }
            };

            var expression = lambda.Body.ToString();
            var body = System.Linq.Dynamic.DynamicExpression.Parse(typeof(S), expression, symbols);

            return Expression.Lambda<Func<T, S>>(body, parameter);
        }

        public static Expression<Func<B, C>> ToExpression<S, B, C>(this Expression<Func<S, C>> expression)
        {
            var visitor = new GenericExpressionVisitor<S, B>();

            var query = (Expression<Func<B, C>>)visitor.Visit(expression);

            return query;
        }

        /// <summary>
        /// See http://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool
        /// </summary>
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> expression1,
            Expression<Func<T, bool>> expression2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expression1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expression1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expression2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expression2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left, right), parameter);
        }

        /// <summary>
        /// See http://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool
        /// </summary>
        public static Expression<Func<T, bool>> Not<T>(
            this Expression<Func<T, bool>> expression)
        {
            var parameter = expression.Parameters[0];
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), parameter);
        }

        /// <summary>
        /// See http://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool
        /// </summary>
        public static Expression<Func<T, bool>> OrElse<T>(
            this Expression<Func<T, bool>> expression1,
            Expression<Func<T, bool>> expression2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expression1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expression1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expression2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expression2.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left, right), parameter);
        }

        /// <summary>
        /// See http://stackoverflow.com/questions/14007101/how-can-i-convert-a-lambda-expression-between-different-but-compatible-models
        /// </summary>
        public static Expression<Func<B, T>> Convert<S, B, T>(this Expression<Func<S, T>> from)
        {
            return ConvertImpl<Func<S, T>, Func<B, T>>(from);
        }

        /// <summary>
        /// See http://stackoverflow.com/questions/14007101/how-can-i-convert-a-lambda-expression-between-different-but-compatible-models
        /// </summary>
        private static Expression<B> ConvertImpl<S, B>(Expression<S> from)
            where S : class
            where B : class
        {
            // figure out which types are different in the function-signature
            var fromTypes = from.Type.GetGenericArguments();
            var toTypes = typeof(B).GetGenericArguments();
            if (fromTypes.Length != toTypes.Length)
            {
                throw new NotSupportedException("Incompatible lambda function-type signatures");
            }

            var typeMap = new Dictionary<Type, Type>();
            for (int i = 0; i < fromTypes.Length; i++)
            {
                if (fromTypes[i] != toTypes[i])
                {
                    typeMap[fromTypes[i]] = toTypes[i];
                }
            }

            // re-map all parameters that involve different types
            var parameterMap = new Dictionary<Expression, Expression>();
            var newParameters = new ParameterExpression[from.Parameters.Count];
            for (int i = 0; i < newParameters.Length; i++)
            {
                Type newType;
                if (typeMap.TryGetValue(from.Parameters[i].Type, out newType))
                {
                    parameterMap[from.Parameters[i]] = newParameters[i] = Expression.Parameter(newType, from.Parameters[i].Name);
                }
                else
                {
                    newParameters[i] = from.Parameters[i];
                }
            }

            // rebuild the lambda
            var body = new TypeConversionVisitor(parameterMap).Visit(from.Body);
            return Expression.Lambda<B>(body, newParameters);
        }
    }
}
