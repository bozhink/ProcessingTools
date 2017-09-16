// <copyright file="IContextProcessor{TContext}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Processor for context object.
    /// </summary>
    /// <typeparam name="TContext">Type of the context object.</typeparam>
    public interface IContextProcessor<in TContext>
    {
        /// <summary>
        /// Executes processing operation over the context.
        /// </summary>
        /// <param name="context">Context object to be processed.</param>
        /// <returns>Task.</returns>
        Task ProcessAsync(TContext context);
    }
}
