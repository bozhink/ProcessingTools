// <copyright file="IExternalLinksDataMiner.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.ExternalLinks
{
    using ProcessingTools.Contracts.Models.ExternalLinks;

    /// <summary>
    /// External links data miner.
    /// </summary>
    public interface IExternalLinksDataMiner : IDataMiner<string, IExternalLink>
    {
    }
}
