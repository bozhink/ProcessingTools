// <copyright file="IExternalLinksHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Harvesters.ExternalLinks
{
    using ProcessingTools.Contracts.Harvesters;
    using ProcessingTools.Models.Contracts.Harvesters.ExternalLinks;

    /// <summary>
    /// External links harvester.
    /// </summary>
    public interface IExternalLinksHarvester : IEnumerableXmlHarvester<IExternalLinkModel>
    {
    }
}
