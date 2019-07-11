// <copyright file="IExternalLinksHarvester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.ExternalLinks
{
    using ProcessingTools.Contracts.Services.Models.ExternalLinks;

    /// <summary>
    /// External links harvester.
    /// </summary>
    public interface IExternalLinksHarvester : IEnumerableXmlHarvester<IExternalLinkModel>
    {
    }
}
