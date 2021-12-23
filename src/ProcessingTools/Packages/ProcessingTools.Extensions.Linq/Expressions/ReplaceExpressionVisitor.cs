// <copyright file="ReplaceExpressionVisitor.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Extensions.Linq.Expressions
{
    using System.Linq.Expressions;

    /// <summary>
    /// Replace expression visitor.
    /// </summary>
    /// <remarks>
    /// See http://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool.
    /// </remarks>
    internal class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression oldValue;
        private readonly Expression newValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceExpressionVisitor"/> class.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        /// <inheritdoc/>
        public override Expression Visit(Expression node)
        {
            if (node is null)
            {
                throw new System.ArgumentNullException(nameof(node));
            }

            if (node == this.oldValue)
            {
                return this.newValue;
            }

            return base.Visit(node);
        }
    }
}
