// <copyright file="IStringDataMinerEvaluator{TMiner}.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Processors.Contracts
{
    using System.Threading.Tasks;
    using ProcessingTools.Contracts;
    using ProcessingTools.Contracts.Data.Miners;

    /// <summary>
    /// Generic string data miner evaluator.
    /// </summary>
    /// <typeparam name="TMiner">Type of data miner.</typeparam>
    public interface IStringDataMinerEvaluator<TMiner>
        where TMiner : IStringDataMiner
    {
        /// <summary>
        /// Executes evaluation of data miner over <see cref="IDocument"/> context.
        /// </summary>
        /// <param name="document"><see cref="IDocument"/> context to be evaluated.</param>
        /// <returns>Array of string data.</returns>
        Task<string[]> EvaluateAsync(IDocument document);
    }
}
