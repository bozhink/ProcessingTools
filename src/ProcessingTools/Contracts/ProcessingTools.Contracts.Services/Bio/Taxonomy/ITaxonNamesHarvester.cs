﻿// <copyright file="ITaxonNamesHarvester.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Contracts.Services.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml;

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
        Task<IList<string>> HarvestLowerTaxaAsync(XmlNode context);

        /// <summary>
        /// Harvest higher taxa.
        /// </summary>
        /// <param name="context">Context to be harvested.</param>
        /// <returns>Array of higher taxon names.</returns>
        Task<IList<string>> HarvestHigherTaxaAsync(XmlNode context);
    }
}
