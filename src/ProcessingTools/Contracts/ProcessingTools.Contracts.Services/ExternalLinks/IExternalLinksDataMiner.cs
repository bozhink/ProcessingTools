// <copyright file="IExternalLinksDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Models.ExternalLinks;

namespace ProcessingTools.Contracts.Services.ExternalLinks
{
    /// <summary>
    /// External links data miner.
    /// </summary>
    public interface IExternalLinksDataMiner : IDataMiner<string, IExternalLink>
    {
    }
}
