// <copyright file="AbstractTaxonRankResolverOverTaxaClassificationResolver.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Abstractions.Bio.Taxonomy
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Abstract taxa rank resolver over taxa classification resolver.
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
        public async Task<ITaxonRank[]> ResolveAsync(params string[] scientificNames)
        {
            return await this.classificationResolver.ResolveAsync(scientificNames).ConfigureAwait(false);
        }
    }
}
