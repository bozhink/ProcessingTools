// <copyright file="GbifTaxonRankResolver.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Bio.Taxonomy.External.GbifApiV09.Services
{
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.External.GbifApiV09.Contracts;

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
