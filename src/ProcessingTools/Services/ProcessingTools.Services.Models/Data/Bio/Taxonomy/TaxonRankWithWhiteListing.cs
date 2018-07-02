// <copyright file="TaxonRankWithWhiteListing.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Models.Data.Bio.Taxonomy
{
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank with white listing service model.
    /// </summary>
    public class TaxonRankWithWhiteListing : TaxonRank, ITaxonRankWithWhiteListing
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonRankWithWhiteListing"/> class.
        /// </summary>
        public TaxonRankWithWhiteListing()
        {
            this.IsWhiteListed = false;
        }

        /// <inheritdoc/>
        public bool IsWhiteListed { get; set; }
    }
}
