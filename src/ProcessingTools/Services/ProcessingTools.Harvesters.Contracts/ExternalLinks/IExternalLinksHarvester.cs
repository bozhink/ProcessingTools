// <copyright file="IExternalLinksHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Harvesters.Contracts.ExternalLinks
{
    using ProcessingTools.Harvesters.Models.Contracts.ExternalLinks;

    /// <summary>
    /// External links harvester.
    /// </summary>
    public interface IExternalLinksHarvester : IEnumerableXmlHarvester<IExternalLinkModel>
    {
    }
}
