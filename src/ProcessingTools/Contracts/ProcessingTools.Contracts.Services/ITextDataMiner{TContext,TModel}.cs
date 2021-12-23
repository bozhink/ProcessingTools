﻿// <copyright file="ITextDataMiner{TContext,TModel}.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Text data miner.
    /// </summary>
    /// <typeparam name="TContext">Type of context to be data-mined.</typeparam>
    /// <typeparam name="TModel">Type of output model.</typeparam>
    public interface ITextDataMiner<TContext, TModel>
    {
        /// <summary>
        /// Data mine context with seed- and stop-words.
        /// </summary>
        /// <param name="context">Context to be data-mined.</param>
        /// <param name="seed">Seed words.</param>
        /// <param name="stopWords">Stop words.</param>
        /// <returns>Data-mined models.</returns>
        Task<TModel[]> MineAsync(TContext context, IEnumerable<string> seed, IEnumerable<string> stopWords);
    }
}
