// <copyright file="ITaxonNamesHarvester.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Harvesters.Bio
{
    using System.Threading.Tasks;
    using System.Xml;
    using ProcessingTools.Contracts.Harvesters;

    /// <summary>
    /// Taxon names harvester.
    /// </summary>
    public interface ITaxonNamesHarvester : IStringEnumerableXmlHarvester
    {
        /// <summary>
        /// Harvest lower taxa.
        /// </summary>
        /// <param name="context">Context to be harvested.</param>
        /// <returns>Array of lower taxon names.</returns>
        Task<string[]> HarvestLowerTaxaAsync(XmlNode context);

        /// <summary>
        /// Harvest higher taxa.
        /// </summary>
        /// <param name="context">Context to be harvested.</param>
        /// <returns>Array of higher taxon names.</returns>
        Task<string[]> HarvestHigherTaxaAsync(XmlNode context);
    }
}
