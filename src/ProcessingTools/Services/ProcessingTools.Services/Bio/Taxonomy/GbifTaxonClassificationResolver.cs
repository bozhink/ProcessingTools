// <copyright file="GbifTaxonClassificationResolver.cs" company="ProcessingTools">
// Copyright (c) 2020 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.Gbif.Json;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Extensions;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    /// <summary>
    /// Taxon rank resolver with GBIF.
    /// </summary>
    public class GbifTaxonClassificationResolver : AbstractTaxonInformationResolver<ITaxonClassificationSearchResult>, IGbifTaxonClassificationResolver
    {
        private readonly IGbifApiV09Client client;

        /// <summary>
        /// Initializes a new instance of the <see cref="GbifTaxonClassificationResolver"/> class.
        /// </summary>
        /// <param name="client">GBIF API v0.9 Client.</param>
        public GbifTaxonClassificationResolver(IGbifApiV09Client client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        protected override async Task<IList<ITaxonClassificationSearchResult>> ResolveNameAsync(string name)
        {
            var result = new HashSet<ITaxonClassificationSearchResult>();

            var response = await this.client.GetDataPerNameAsync(name).ConfigureAwait(false);

            if ((response != null) &&
                (!string.IsNullOrWhiteSpace(response.CanonicalName) || !string.IsNullOrWhiteSpace(response.ScientificName)) &&
                (response.CanonicalName.Equals(name, StringComparison.InvariantCultureIgnoreCase) || response.ScientificName.Contains(name)))
            {
                result.Add(this.MapGbifTaxonToTaxonClassification(response));

                if (response.Alternatives != null)
                {
                    response.Alternatives
                        .Where(a => a.CanonicalName.Equals(name, StringComparison.InvariantCultureIgnoreCase) || a.ScientificName.Contains(name))
                        .Select(this.MapGbifTaxonToTaxonClassification)
                        .ToList()
                        .ForEach(a => result.Add(a));
                }
            }

            return result.ToArray();
        }

        private ITaxonClassificationSearchResult MapGbifTaxonToTaxonClassification(IGbifTaxon taxon)
        {
            var result = new TaxonClassificationSearchResult
            {
                ScientificName = taxon.ScientificName,
                CanonicalName = taxon.CanonicalName,
                Rank = taxon.Rank.MapTaxonRankStringToTaxonRankType(),
            };

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Kingdom,
                ScientificName = taxon.Kingdom,
            });

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Phylum,
                ScientificName = taxon.Phylum,
            });

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Class,
                ScientificName = taxon.Class,
            });

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Order,
                ScientificName = taxon.Order,
            });

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Family,
                ScientificName = taxon.Family,
            });

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Genus,
                ScientificName = taxon.Genus,
            });

            return result;
        }
    }
}
