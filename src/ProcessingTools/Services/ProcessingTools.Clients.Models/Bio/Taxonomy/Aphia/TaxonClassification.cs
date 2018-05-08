// <copyright file="TaxonClassification.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Models.Bio.Taxonomy.Aphia
{
    using System.Collections.Generic;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Taxon classification.
    /// </summary>
    public class TaxonClassification : ITaxonClassification
    {
        /// <inheritdoc/>
        public string ScientificName { get; set; }

        /// <inheritdoc/>
        public string CanonicalName { get; set; }

        /// <inheritdoc/>
        public string Authority { get; set; }

        /// <inheritdoc/>
        public TaxonRankType Rank { get; set; }

        /// <inheritdoc/>
        public ICollection<ITaxonRank> Classification { get; set; }
    }
}
