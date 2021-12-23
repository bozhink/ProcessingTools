// <copyright file="CatalogueOfLifeTaxonRankResolver.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank resolver with Catalogue of Life.
    /// </summary>
    public class CatalogueOfLifeTaxonRankResolver : AbstractTaxonRankResolverOverTaxaClassificationResolver, ICatalogueOfLifeTaxonRankResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogueOfLifeTaxonRankResolver"/> class.
        /// </summary>
        /// <param name="classificationResolver">Classification resolver.</param>
        public CatalogueOfLifeTaxonRankResolver(ICatalogueOfLifeTaxonClassificationResolver classificationResolver)
            : base(classificationResolver)
        {
        }
    }
}
