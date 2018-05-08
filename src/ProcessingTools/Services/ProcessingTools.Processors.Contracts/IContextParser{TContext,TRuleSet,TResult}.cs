// <copyright file="IContextParser{TContext,TRuleSet,TResult}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Generic context parser.
    /// </summary>
    /// <typeparam name="TContext">Type of the context object.</typeparam>
    /// <typeparam name="TRuleSet">Type of rule sets to apply.</typeparam>
    /// <typeparam name="TResult">Type of the result.</typeparam>
    public interface IContextParser<in TContext, in TRuleSet, TResult>
    {
        /// <summary>
        /// Executes parsing operation over the context.
        /// </summary>
        /// <param name="context">Context object to be processed.</param>
        /// <param name="ruleSets">Rule sets to apply.</param>
        /// <returns>Task of result.</returns>
        Task<TResult> ParseAsync(TContext context, IEnumerable<TRuleSet> ruleSets);
    }
}
