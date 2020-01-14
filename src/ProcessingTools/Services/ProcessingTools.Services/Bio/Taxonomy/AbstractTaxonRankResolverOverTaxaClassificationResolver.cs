// <copyright file="AbstractTaxonRankResolverOverTaxaClassificationResolver.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
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
        public async Task<IList<ITaxonRankSearchResult>> ResolveAsync(IEnumerable<string> names)
        {
            var classifications = await this.classificationResolver.ResolveAsync(names).ConfigureAwait(false);

            if (classifications != null && classifications.Any())
            {
                return classifications.ToArray<ITaxonRankSearchResult>();
            }

            return Array.Empty<ITaxonRankSearchResult>();
        }
    }
}
