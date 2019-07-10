// <copyright file="ExpressionBuilder{T}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.DataAccess.Expressions;

namespace ProcessingTools.Common.Code.Data.Expressions
{
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
