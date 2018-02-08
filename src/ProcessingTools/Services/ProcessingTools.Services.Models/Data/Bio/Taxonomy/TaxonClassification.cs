// <copyright file="TaxonClassification.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Bio.Taxonomy
{
    using System.Collections.Generic;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Taxon classification service model.
    /// </summary>
    public class TaxonClassification : ITaxonClassification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonClassification"/> class.
        /// </summary>
        public TaxonClassification()
        {
            this.Classification = new List<ITaxonRank>();
        }

        /// <inheritdoc/>
        public string Authority { get; set; }

        /// <inheritdoc/>
        public string CanonicalName { get; set; }

        /// <inheritdoc/>
        public ICollection<ITaxonRank> Classification { get; private set; }

        /// <inheritdoc/>
        public TaxonRankType Rank { get; set; }

        /// <inheritdoc/>
        public string ScientificName { get; set; }
    }
}
