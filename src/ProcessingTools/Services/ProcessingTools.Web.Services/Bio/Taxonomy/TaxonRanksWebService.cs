// <copyright file="TaxonRanksWebService.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Web.Services.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Code.Extensions;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Contracts.Web.Services.Bio.Taxonomy;
    using ProcessingTools.Web.Models.Bio.Taxonomy.TaxonRanks;

    /// <summary>
    /// Taxon ranks web service.
    /// </summary>
    public class TaxonRanksWebService : ITaxonRanksWebService
    {
        private readonly ITaxonRankDataService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonRanksWebService"/> class.
        /// </summary>
        /// <param name="service">Data service.</param>
        public TaxonRanksWebService(ITaxonRankDataService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <inheritdoc/>
        public Task<object> InsertAsync(TaxonRanksRequestModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.InsertInternalAsync(model);
        }

        /// <inheritdoc/>
        public Task<SearchResponseModel> SearchAsync(SearchRequestModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return this.SearchInternalAsync(model);
        }

        private async Task<object> InsertInternalAsync(TaxonRanksRequestModel model)
        {
            if (model.Items != null && model.Items.Any())
            {
                var items = model.Items
                   .Select(i => new TaxonRankModel
                   {
                       ScientificName = i.Name,
                       Rank = i.Rank.MapTaxonRankStringToTaxonRankType(),
                   })
                   .ToArray();

                var result = await this.service.InsertAsync(items).ConfigureAwait(false);

                return result;
            }

            return null;
        }

        private async Task<SearchResponseModel> SearchInternalAsync(SearchRequestModel model)
        {
            var foundItems = await this.service.SearchAsync(model.SearchString).ConfigureAwait(false);

            if (foundItems != null && foundItems.Any())
            {
                var items = foundItems
                    .Select(t => new TaxonRankResponseModel
                    {
                        Name = t.ScientificName,
                        Rank = t.Rank.MapTaxonRankTypeToTaxonRankString(),
                    })
                    .ToArray();

                return new SearchResponseModel(items);
            }

            return null;
        }
    }
}
