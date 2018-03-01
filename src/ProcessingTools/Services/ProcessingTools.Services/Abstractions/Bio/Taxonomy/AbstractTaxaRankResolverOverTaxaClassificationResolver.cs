// <copyright file="AbstractTaxaRankResolverOverTaxaClassificationResolver.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
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
    public abstract class AbstractTaxaRankResolverOverTaxaClassificationResolver : ITaxaRankResolver
    {
        private readonly ITaxaClassificationResolver classificationResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractTaxaRankResolverOverTaxaClassificationResolver"/> class.
        /// </summary>
        /// <param name="classificationResolver">classification resolver.</param>
        protected AbstractTaxaRankResolverOverTaxaClassificationResolver(ITaxaClassificationResolver classificationResolver)
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
