// <copyright file="IDataMiner{TContext,TModel}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Contracts
{
    using System.Threading.Tasks;

    /// <summary>
    /// Generic data miner.
    /// </summary>
    /// <typeparam name="TContext">Type of context to be data-mined.</typeparam>
    /// <typeparam name="TModel">Type of output model.</typeparam>
    public interface IDataMiner<TContext, TModel>
    {
        /// <summary>
        /// Data-mine specified context.
        /// </summary>
        /// <param name="context">Context to be data-mined.</param>
        /// <returns>Array of output model.</returns>
        Task<TModel[]> MineAsync(TContext context);
    }
}
