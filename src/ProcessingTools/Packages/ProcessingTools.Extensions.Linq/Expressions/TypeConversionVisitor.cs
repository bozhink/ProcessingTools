// <copyright file="TypeConversionVisitor.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq.Expressions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Type conversion visitor.
    /// </summary>
    /// <remarks>
    /// See http://stackoverflow.com/questions/14007101/how-can-i-convert-a-lambda-expression-between-different-but-compatible-models.
    /// </remarks>
    internal class TypeConversionVisitor : ExpressionVisitor
    {
        private readonly IDictionary<Expression, Expression> parameterMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeConversionVisitor"/> class.
        /// </summary>
        /// <param name="parameterMap">The parameter map.</param>
        public TypeConversionVisitor(IDictionary<Expression, Expression> parameterMap)
        {
            this.parameterMap = parameterMap ?? new Dictionary<Expression, Expression>();
        }

        /// <inheritdoc/>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (this.parameterMap.TryGetValue(node, out Expression parameter))
            {
                return parameter;
            }

            return base.VisitParameter(node);
        }

        /// <inheritdoc/>
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node is null)
            {
                throw new System.ArgumentNullException(nameof(node));
            }

            // re-perform any member-binding
            var expression = this.Visit(node.Expression);
            if (expression.Type != node.Type)
            {
                var newMember = expression.Type
                    .GetMember(node.Member.Name)
                    .Single();

                return Expression.MakeMemberAccess(expression, newMember);
            }

            return base.VisitMember(node);
        }
    }
}
