// <copyright file="IParseContextStrategy{TContext,TResult}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Strategies
{
    using System.Threading.Tasks;

    /// <summary>
    /// Strategy to parse specified context.
    /// </summary>
    /// <typeparam name="TContext">Type of the context object</typeparam>
    /// <typeparam name="TResult">Type of the output result</typeparam>
    public interface IParseContextStrategy<in TContext, TResult> : IStrategy
    {
        /// <summary>
        /// Executes parsing operation over the context.
        /// </summary>
        /// <param name="context">Context object to be processed.</param>
        /// <returns>Task of result.</returns>
        Task<TResult> ParseAsync(TContext context);
    }
}
