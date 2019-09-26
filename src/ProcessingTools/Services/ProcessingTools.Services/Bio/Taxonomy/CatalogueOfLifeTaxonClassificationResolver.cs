// <copyright file="CatalogueOfLifeTaxonClassificationResolver.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Extensions;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    /// <summary>
    /// Taxon classification resolver with Catalogue of Life.
    /// </summary>
    public class CatalogueOfLifeTaxonClassificationResolver : AbstractTaxonInformationResolver<ITaxonClassification>, ICatalogueOfLifeTaxonClassificationResolver
    {
        private readonly ICatalogueOfLifeWebserviceClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatalogueOfLifeTaxonClassificationResolver"/> class.
        /// </summary>
        /// <param name="client">Webservice client.</param>
        public CatalogueOfLifeTaxonClassificationResolver(ICatalogueOfLifeWebserviceClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        protected override async Task<ITaxonClassification[]> ResolveScientificNameAsync(string scientificName)
        {
            var response = await this.client.GetDataPerNameAsync(scientificName).ConfigureAwait(false);

            if (response is null || response.Results is null || response.Results.Length < 1)
            {
                return Array.Empty<ITaxonClassification>();
            }

            return response.Results
                .Where(r => r != null && r.Name == scientificName)
                .Select(this.MapResultToClassification)
                .ToArray();
        }

        private ITaxonClassification MapResultToClassification(CatalogueOfLifeApiServiceXmlResponseModel.Result result)
        {
            var taxonClassification = new TaxonClassification
            {
                ScientificName = result.Name,
                Rank = result.Rank.MapTaxonRankStringToTaxonRankType(),
                Authority = result.Author,
                CanonicalName = result.AcceptedName?.Name,
            };

            foreach (var rank in Enum.GetValues(typeof(TaxonRankType)).Cast<TaxonRankType>())
            {
                var taxon = this.GetClassificationItem(result, rank);
                if (taxon != null)
                {
                    taxonClassification.Classification.Add(taxon);
                }
            }

            return taxonClassification;
        }

        private ITaxonRank GetClassificationItem(CatalogueOfLifeApiServiceXmlResponseModel.AcceptedName result, TaxonRankType rank)
        {
            if (result is null || result.Classification is null || result.Classification.Length < 1)
            {
                return null;
            }

            var rankString = rank.MapTaxonRankTypeToTaxonRankString();
            var first = result.Classification.FirstOrDefault(c => string.Compare(c.Rank, rankString, true, CultureInfo.InvariantCulture) == 0);
            var name = first?.Name;

            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            return new TaxonRank
            {
                ScientificName = name,
                Rank = rank,
            };
        }
    }
}
