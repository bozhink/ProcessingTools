// <copyright file="ITextDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Data.Miners
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITextDataMiner<TContext, T>
    {
        Task<IEnumerable<T>> MineAsync(TContext context, IEnumerable<string> seed, IEnumerable<string> stopWords);
    }
}
