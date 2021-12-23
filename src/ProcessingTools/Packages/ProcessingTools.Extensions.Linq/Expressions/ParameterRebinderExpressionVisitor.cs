// <copyright file="ParameterRebinderExpressionVisitor.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq.Expressions
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Parameter rebinder expression visitor.
    /// </summary>
    /// <remarks>
    /// See https://blogs.msdn.microsoft.com/meek/2008/05/02/linq-to-entities-combining-predicates/.
    /// </remarks>
    public class ParameterRebinderExpressionVisitor : ExpressionVisitor
    {
        private readonly IDictionary<ParameterExpression, ParameterExpression> map;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterRebinderExpressionVisitor"/> class.
        /// </summary>
        /// <param name="map">Binding map.</param>
        public ParameterRebinderExpressionVisitor(IDictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        /// <summary>
        /// Replace parameters.
        /// </summary>
        /// <param name="map">Binding map.</param>
        /// <param name="expression">Expression to be processed.</param>
        /// <returns>Processed expression.</returns>
        public static Expression ReplaceParameters(IDictionary<ParameterExpression, ParameterExpression> map, Expression expression)
        {
            return new ParameterRebinderExpressionVisitor(map).Visit(expression);
        }

        /// <inheritdoc/>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node is null)
            {
                throw new System.ArgumentNullException(nameof(node));
            }

            if (this.map.TryGetValue(node, out ParameterExpression replacement))
            {
                node = replacement;
            }

            return base.VisitParameter(node);
        }
    }
}
