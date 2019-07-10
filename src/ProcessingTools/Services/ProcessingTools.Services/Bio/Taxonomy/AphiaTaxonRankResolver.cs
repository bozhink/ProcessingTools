// <copyright file="AphiaTaxonRankResolver.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Bio.Taxonomy;

namespace ProcessingTools.Services.Bio.Taxonomy
{
    /// <summary>
    /// Taxon rank resolver with Aphia.
    /// </summary>
    public class AphiaTaxonRankResolver : AbstractTaxonRankResolverOverTaxaClassificationResolver, IAphiaTaxonRankResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AphiaTaxonRankResolver"/> class.
        /// </summary>
        /// <param name="classificationResolver">Classification resolver.</param>
        public AphiaTaxonRankResolver(IAphiaTaxonClassificationResolver classificationResolver)
            : base(classificationResolver)
        {
        }
    }
}
