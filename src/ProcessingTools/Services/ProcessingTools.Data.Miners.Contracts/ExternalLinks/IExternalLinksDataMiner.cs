// <copyright file="IExternalLinksDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Miners.Contracts.ExternalLinks
{
    using ProcessingTools.Data.Miners.Models.Contracts.ExternalLinks;

    /// <summary>
    /// External links data miner.
    /// </summary>
    public interface IExternalLinksDataMiner : IDataMiner<string, IExternalLink>
    {
    }
}
