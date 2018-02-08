// <copyright file="IExternalLinksDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Contracts.Miners.ExternalLinks
{
    using ProcessingTools.Contracts.Data.Miners;
    using ProcessingTools.Data.Miners.Contracts.Models.ExternalLinks;

    /// <summary>
    /// External links data miner.
    /// </summary>
    public interface IExternalLinksDataMiner : IDataMiner<string, IExternalLink>
    {
    }
}
