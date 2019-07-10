// <copyright file="IContextParser{TContext,TResult}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;

namespace ProcessingTools.Contracts.Services
{
    /// <summary>
    /// Parse content in context object.
    /// </summary>
    /// <typeparam name="TContext">Type of the context object.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    public interface IContextParser<in TContext, TResult>
    {
        /// <summary>
        /// Executes parsing operation over the context.
        /// </summary>
        /// <param name="context">Context object to be processed.</param>
        /// <returns>Task of result.</returns>
        Task<TResult> ParseAsync(TContext context);
    }
}
