﻿// <copyright file="GbifTaxonClassificationResolver.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
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
    public class GbifTaxonClassificationResolver : AbstractTaxonInformationResolver<ITaxonClassification>, IGbifTaxonClassificationResolver
    {
        private readonly IGbifApiV09DataRequester requester;

        /// <summary>
        /// Initializes a new instance of the <see cref="GbifTaxonClassificationResolver"/> class.
        /// </summary>
        /// <param name="requester">Data requester.</param>
        public GbifTaxonClassificationResolver(IGbifApiV09DataRequester requester)
        {
            this.requester = requester ?? throw new ArgumentNullException(nameof(requester));
        }

        /// <inheritdoc/>
        protected override async Task<ITaxonClassification[]> ResolveScientificNameAsync(string scientificName)
        {
            var result = new HashSet<ITaxonClassification>();

            var response = await this.requester.RequestDataAsync(scientificName).ConfigureAwait(false);

            if ((response != null) &&
                (!string.IsNullOrWhiteSpace(response.CanonicalName) || !string.IsNullOrWhiteSpace(response.ScientificName)) &&
                (response.CanonicalName.Equals(scientificName) || response.ScientificName.Contains(scientificName)))
            {
                result.Add(this.MapGbifTaxonToTaxonClassification(response));

                if (response.Alternatives != null)
                {
                    response.Alternatives
                        .Where(a => a.CanonicalName.Equals(scientificName) || a.ScientificName.Contains(scientificName))
                        .Select(this.MapGbifTaxonToTaxonClassification)
                        .ToList()
                        .ForEach(a => result.Add(a));
                }
            }

            return result.ToArray();
        }

        private ITaxonClassification MapGbifTaxonToTaxonClassification(IGbifTaxon taxon)
        {
            var result = new TaxonClassification
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
