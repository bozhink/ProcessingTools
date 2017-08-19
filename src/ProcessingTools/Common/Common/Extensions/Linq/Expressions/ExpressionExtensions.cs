namespace ProcessingTools.Common.Extensions.Linq.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

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
        /// <typeparam name="T">Input type</typeparam>
        /// <param name="expression1">Left expression to be concatenated</param>
        /// <param name="expression2">Right expression to be concatenated</param>
        /// <returns>Concatenated expressions with AndAlso operator</returns>
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
        /// See http://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool
        /// </summary>
        /// <typeparam name="T">Input type</typeparam>
        /// <param name="expression">Input expression</param>
        /// <returns>Not operator over expression</returns>
        public static Expression<Func<T, bool>> Not<T>(
            this Expression<Func<T, bool>> expression)
        {
            var parameter = expression.Parameters[0];
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), parameter);
        }

        /// <summary>
        /// See http://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool
        /// </summary>
        /// <typeparam name="T">Input type</typeparam>
        /// <param name="expression1">Left expression to be concatenated</param>
        /// <param name="expression2">Right expression to be concatenated</param>
        /// <returns>Concatenated expressions with OrElse operator</returns>
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

        // See http://stackoverflow.com/questions/14007101/how-can-i-convert-a-lambda-expression-between-different-but-compatible-models
        public static Expression<Func<B, T>> Convert<S, B, T>(this Expression<Func<S, T>> from)
        {
            return ConvertImpl<Func<S, T>, Func<B, T>>(from);
        }

        // See http://stackoverflow.com/questions/14007101/how-can-i-convert-a-lambda-expression-between-different-but-compatible-models
        private static Expression<B> ConvertImpl<S, B>(Expression<S> from)
            where S : class
            where B : class
        {
            // figure out which types are different in the function-signature
            var mapfromTypes = from.Type.GetGenericArguments();
            var maptoTypes = typeof(B).GetGenericArguments();
            if (mapfromTypes.Length != maptoTypes.Length)
            {
                throw new NotSupportedException("Incompatible lambda function-type signatures");
            }

            var typeMap = new Dictionary<Type, Type>();
            for (int i = 0; i < mapfromTypes.Length; i++)
            {
                if (mapfromTypes[i] != maptoTypes[i])
                {
                    typeMap[mapfromTypes[i]] = maptoTypes[i];
                }
            }

            // re-map all parameters that involve different types
            var parameterMap = new Dictionary<Expression, Expression>();
            var newParameters = new ParameterExpression[from.Parameters.Count];
            for (int i = 0; i < newParameters.Length; i++)
            {
                var parameter = from.Parameters[i];
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
            var body = new TypeConversionVisitor(parameterMap).Visit(from.Body);
            return Expression.Lambda<B>(body, newParameters);
        }
    }
}
