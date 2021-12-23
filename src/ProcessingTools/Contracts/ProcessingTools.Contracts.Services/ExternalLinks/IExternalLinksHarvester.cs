// <copyright file="IExternalLinksHarvester.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.ExternalLinks
{
    using ProcessingTools.Contracts.Models.ExternalLinks;

    /// <summary>
    /// External links harvester.
    /// </summary>
    public interface IExternalLinksHarvester : IEnumerableXmlHarvester<IExternalLinkModel>
    {
    }
}
