// <copyright file="GbifTaxonRankResolver.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank resolver with GBIF.
    /// </summary>
    public class GbifTaxonRankResolver : AbstractTaxonRankResolverOverTaxaClassificationResolver, IGbifTaxonRankResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GbifTaxonRankResolver"/> class.
        /// </summary>
        /// <param name="classificationResolver">Classification resolver.</param>
        public GbifTaxonRankResolver(IGbifTaxonClassificationResolver classificationResolver)
            : base(classificationResolver)
        {
        }
    }
}
