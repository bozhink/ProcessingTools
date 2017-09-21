// <copyright file="INormalizer{TContext,TResult}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Generic normalizer for specified context.
    /// </summary>
    /// <typeparam name="TContext">Type of the context of normalization</typeparam>
    /// <typeparam name="TResult">Type of the output result</typeparam>
    public interface INormalizer<in TContext, TResult> : ITransformer
    {
        /// <summary>
        /// Asynchronously normalize specified context.
        /// </summary>
        /// <param name="context">Context object to be normalized</param>
        /// <returns>Task of output result</returns>
        Task<TResult> NormalizeAsync(TContext context);
    }
}
