// <copyright file="IContextFormatter{TContext,TResult}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Format context object.
    /// </summary>
    /// <typeparam name="TContext">Type of the context object.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    public interface IContextFormatter<in TContext, TResult>
    {
        /// <summary>
        /// Executes formatting operation over the context.
        /// </summary>
        /// <param name="context">Context object to be processed.</param>
        /// <returns>Task of result.</returns>
        Task<TResult> FormatAsync(TContext context);
    }
}
