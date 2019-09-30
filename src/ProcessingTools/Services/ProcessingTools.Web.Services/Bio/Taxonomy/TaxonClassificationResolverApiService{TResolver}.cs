// <copyright file="TaxonClassificationResolverApiService{TResolver}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Contracts.Web.Services.Bio.Taxonomy;
    using ProcessingTools.Web.Models.Bio.Taxonomy;

    /// <summary>
    /// Generic taxon classification resolver API service.
    /// </summary>
    /// <typeparam name="TResolver">Type of the classification resolver.</typeparam>
    public class TaxonClassificationResolverApiService<TResolver> : ITaxonClassificationResolverApiService<TResolver>
        where TResolver : class, ITaxonClassificationResolver
    {
        private readonly TResolver resolver;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonClassificationResolverApiService{TResolver}"/> class.
        /// </summary>
        /// <param name="resolver">Instance of <see cref="ITaxonClassificationResolver"/>.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public TaxonClassificationResolverApiService(TResolver resolver, IMapper mapper)
        {
            this.resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public async Task<IList<TaxonClassificationResponseModel>> ResolveAsync(IEnumerable<string> taxonNames)
        {
            if (taxonNames is null || !taxonNames.Any())
            {
                return Array.Empty<TaxonClassificationResponseModel>();
            }

            var classifications = await this.resolver.ResolveAsync(taxonNames).ConfigureAwait(false);

            if (classifications is null || !classifications.Any())
            {
                return Array.Empty<TaxonClassificationResponseModel>();
            }

            return classifications.Select(this.mapper.Map<ITaxonClassificationSearchResult, TaxonClassificationResponseModel>).ToArray();
        }
    }
}
