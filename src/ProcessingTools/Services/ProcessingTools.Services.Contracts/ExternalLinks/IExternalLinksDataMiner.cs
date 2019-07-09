// <copyright file="IExternalLinksDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Contracts.ExternalLinks
{
    using ProcessingTools.Services.Models.Contracts.ExternalLinks;

    /// <summary>
    /// External links data miner.
    /// </summary>
    public interface IExternalLinksDataMiner : IDataMiner<string, IExternalLink>
    {
    }
}
