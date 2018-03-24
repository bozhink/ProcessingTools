// <copyright file="ExpressionBuilder{T}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Common.Data.Expressions
{
    using ProcessingTools.Contracts.Data.Expressions;

    /// <summary>
    /// Generic expression builder.
    /// </summary>
    /// <typeparam name="T">Type of the updated object.</typeparam>
    public static class ExpressionBuilder<T>
    {
        /// <summary>
        /// Gets update expression.
        /// </summary>
        public static IUpdateExpression<T> UpdateExpression => new UpdateExpression<T>();
    }
}
