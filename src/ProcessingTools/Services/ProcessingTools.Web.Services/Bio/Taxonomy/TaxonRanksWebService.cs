// <copyright file="TaxonRanksWebService.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

using ProcessingTools.Contracts.Services.Bio.Taxonomy;
using ProcessingTools.Contracts.Web.Services.Bio.Taxonomy;

namespace ProcessingTools.Web.Services.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Extensions;
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
        public async Task<object> InsertAsync(TaxonRanksRequestModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

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

        /// <inheritdoc/>
        public async Task<SearchResponseModel> SearchAsync(SearchRequestModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

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
