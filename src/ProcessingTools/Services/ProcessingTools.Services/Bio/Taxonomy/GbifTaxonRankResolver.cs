// <copyright file="GbifTaxonRankResolver.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Bio.Taxonomy;

namespace ProcessingTools.Services.Bio.Taxonomy
{
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
