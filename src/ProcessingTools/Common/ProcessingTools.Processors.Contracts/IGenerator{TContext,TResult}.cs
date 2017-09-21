// <copyright file="IGenerator{TContext,TResult}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Generic context generator.
    /// </summary>
    /// <typeparam name="TContext">Type of context object</typeparam>
    /// <typeparam name="TResult">Type of result object</typeparam>
    public interface IGenerator<in TContext, TResult>
    {
        /// <summary>
        /// Asynchronously run generator over a specified context.
        /// </summary>
        /// <param name="context">Context for the generator</param>
        /// <returns>Task of result object</returns>
        Task<TResult> GenerateAsync(TContext context);
    }
}
