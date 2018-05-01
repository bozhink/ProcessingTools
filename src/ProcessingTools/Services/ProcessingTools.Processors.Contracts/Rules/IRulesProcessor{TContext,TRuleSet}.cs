// <copyright file="IRulesProcessor{TContext,TRuleSet}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts.Rules
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Rules;

    /// <summary>
    /// Rules processor.
    /// </summary>
    /// <typeparam name="TContext">Type of the context object.</typeparam>
    /// <typeparam name="TRuleSet">Type of the rule set model.</typeparam>
    public interface IRulesProcessor<in TContext, in TRuleSet>
        where TRuleSet : IRuleSetModel
    {
        /// <summary>
        /// Processes context by applying specified rule sets.
        /// </summary>
        /// <param name="context">Context to be processed.</param>
        /// <param name="ruleSets">Rule sets to be applied.</param>
        /// <returns>Resultant object.</returns>
        Task<object> ProcessAsync(TContext context, IEnumerable<TRuleSet> ruleSets);
    }
}
