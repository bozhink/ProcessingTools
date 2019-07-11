// <copyright file="AbstractTaxonRankResolverOverTaxaClassificationResolver.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// Abstract taxon rank resolver over taxon classification resolver.
    /// </summary>
    public abstract class AbstractTaxonRankResolverOverTaxaClassificationResolver : ITaxonRankResolver
    {
        private readonly ITaxonClassificationResolver classificationResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractTaxonRankResolverOverTaxaClassificationResolver"/> class.
        /// </summary>
        /// <param name="classificationResolver">classification resolver.</param>
        protected AbstractTaxonRankResolverOverTaxaClassificationResolver(ITaxonClassificationResolver classificationResolver)
        {
            this.classificationResolver = classificationResolver ?? throw new ArgumentNullException(nameof(classificationResolver));
        }

        /// <inheritdoc/>
        public async Task<IList<ITaxonRank>> ResolveAsync(IEnumerable<string> scientificNames)
        {
            var classifications = await this.classificationResolver.ResolveAsync(scientificNames).ConfigureAwait(false);

            if (classifications != null && classifications.Any())
            {
                return classifications.ToArray<ITaxonRank>();
            }

            return Array.Empty<ITaxonRank>();
        }
    }
}
