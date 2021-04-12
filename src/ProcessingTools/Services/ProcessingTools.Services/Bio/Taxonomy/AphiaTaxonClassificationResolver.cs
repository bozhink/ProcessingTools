// <copyright file="AphiaTaxonClassificationResolver.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// Taxon classification resolver with Aphia.
    /// </summary>
    public class AphiaTaxonClassificationResolver : AbstractTaxonInformationResolver<ITaxonClassificationSearchResult>, IAphiaTaxonClassificationResolver
    {
        private readonly IAphiaTaxonClassificationRequester requester;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AphiaTaxonClassificationResolver"/> class.
        /// </summary>
        /// <param name="requester">Data requester.</param>
        /// <param name="mapper">Instance of <see cref="IMapper"/>.</param>
        public AphiaTaxonClassificationResolver(IAphiaTaxonClassificationRequester requester, IMapper mapper)
        {
            this.requester = requester ?? throw new ArgumentNullException(nameof(requester));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        protected override async Task<IList<ITaxonClassificationSearchResult>> ResolveNameAsync(string name)
        {
            var response = await this.requester.ResolveScientificNameAsync(name).ConfigureAwait(false);

            if (response is null || response.Count < 1)
            {
                return Array.Empty<ITaxonClassificationSearchResult>();
            }

            return response.Select(this.mapper.Map<ITaxonClassification, ITaxonClassificationSearchResult>).ToArray();
        }
    }
}
