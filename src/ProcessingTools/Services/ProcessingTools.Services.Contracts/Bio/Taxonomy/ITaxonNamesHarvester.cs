// <copyright file="ITaxonNamesHarvester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using System.Xml;

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
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
