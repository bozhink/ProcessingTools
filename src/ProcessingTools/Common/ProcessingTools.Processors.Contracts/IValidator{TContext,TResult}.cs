// <copyright file="IValidator{TContext,TResult}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;

    /// <summary>
    /// Generic validator.
    /// </summary>
    /// <typeparam name="TContext">Type of context to be validated</typeparam>
    /// <typeparam name="TResult">Type of output object</typeparam>
    public interface IValidator<in TContext, TResult>
    {
        /// <summary>
        /// Asynchronously validate specified context.
        /// </summary>
        /// <param name="context">Context to be validated</param>
        /// <param name="reporter"><see cref="IReporter"/> object to build validation report</param>
        /// <returns>Task of output object</returns>
        Task<TResult> ValidateAsync(TContext context, IReporter reporter);
    }
}
