// <copyright file="AphiaTaxonClassificationResolver.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Threading.Tasks;
    using ProcessingTools.Clients.Contracts.Bio.Taxonomy;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;

    /// <summary>
    /// Taxon classification resolver with Aphia.
    /// </summary>
    public class AphiaTaxonClassificationResolver : AbstractTaxonInformationResolver<ITaxonClassification>, IAphiaTaxonClassificationResolver
    {
        private readonly IAphiaTaxonClassificationRequester requester;

        /// <summary>
        /// Initializes a new instance of the <see cref="AphiaTaxonClassificationResolver"/> class.
        /// </summary>
        /// <param name="requester">Data requester.</param>
        public AphiaTaxonClassificationResolver(IAphiaTaxonClassificationRequester requester)
        {
            this.requester = requester ?? throw new ArgumentNullException(nameof(requester));
        }

        /// <inheritdoc/>
        protected override async Task<ITaxonClassification[]> ResolveScientificNameAsync(string scientificName)
        {
            var result = await this.requester.ResolveScientificNameAsync(scientificName).ConfigureAwait(false);

            return result;
        }
    }
}
