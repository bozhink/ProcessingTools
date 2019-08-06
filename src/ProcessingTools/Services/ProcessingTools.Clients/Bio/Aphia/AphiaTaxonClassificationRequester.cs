﻿// <copyright file="AphiaTaxonClassificationRequester.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Bio.Aphia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Clients.ConnectedServices.Bio.Aphia;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.Aphia;
    using ProcessingTools.Common.Enumerations;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;
    using ProcessingTools.Extensions;

    /// <summary>
    /// Aphia taxon classification requester.
    /// </summary>
    public class AphiaTaxonClassificationRequester : IAphiaTaxonClassificationRequester
    {
        /// <inheritdoc/>
        public async Task<ITaxonClassification[]> ResolveScientificNameAsync(string scientificName)
        {
            if (string.IsNullOrWhiteSpace(scientificName))
            {
                throw new ArgumentNullException(nameof(scientificName));
            }

            var aphiaRecords = await this.GetAphiaRecordsAsync(scientificName);

            var result = new HashSet<ITaxonClassification>();

            if (aphiaRecords != null && aphiaRecords.@return.Length > 0)
            {
                var records = aphiaRecords.@return
                    .Where(s => string.Compare(s.scientificname, scientificName, true) == 0)
                    .Select(this.MapAphiaRecordToTaxonClassification);

                foreach (var record in records)
                {
                    result.Add(record);
                }
            }

            return result.ToArray();
        }

        private async Task<getAphiaRecordsResponse> GetAphiaRecordsAsync(string scientificName)
        {
            var client = new AphiaNameServicePortTypeClient();

            await client.OpenAsync().ConfigureAwait(false);

            var aphiaRecords = await client.getAphiaRecordsAsync(new getAphiaRecordsRequest(scientificName, false, true, false, 0)).ConfigureAwait(false);

            await client.CloseAsync().ConfigureAwait(false);

            return aphiaRecords;
        }

        private ITaxonClassification MapAphiaRecordToTaxonClassification(AphiaRecord record)
        {
            var result = new TaxonClassification
            {
                Rank = record.rank.MapTaxonRankStringToTaxonRankType(),
                ScientificName = record.scientificname,
                Authority = record.authority,
                CanonicalName = record.valid_name,
                Classification = new HashSet<ITaxonRank>(),
            };

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Kingdom,
                ScientificName = record.kingdom,
            });

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Phylum,
                ScientificName = record.phylum,
            });

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Class,
                ScientificName = record.@class,
            });

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Order,
                ScientificName = record.order,
            });

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Family,
                ScientificName = record.family,
            });

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Genus,
                ScientificName = record.genus,
            });

            return result;
        }
    }
}