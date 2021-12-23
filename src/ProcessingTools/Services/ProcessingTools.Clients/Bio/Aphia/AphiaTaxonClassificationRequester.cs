// <copyright file="AphiaTaxonClassificationRequester.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Clients.Bio.Aphia
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Common;
    using ProcessingTools.Bio.Taxonomy.Contracts.Models;
    using ProcessingTools.Clients.ConnectedServices.Bio.AphiaServiceReference;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.Aphia;
    using ProcessingTools.Contracts.Services.Bio.Taxonomy;

    /// <summary>
    /// Aphia taxon classification requester.
    /// </summary>
    public class AphiaTaxonClassificationRequester : IAphiaTaxonClassificationRequester
    {
        /// <inheritdoc/>
        public async Task<IList<ITaxonClassification>> ResolveScientificNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Array.Empty<ITaxonClassification>();
            }

            var aphiaRecords = await this.GetAphiaRecordsAsync(name).ConfigureAwait(false);

            var result = new HashSet<ITaxonClassification>();

            if (aphiaRecords != null && aphiaRecords.@return.Length > 0)
            {
                var records = aphiaRecords.@return
                    .Where(s => string.Compare(s.scientificname, name, true, CultureInfo.InvariantCulture) == 0)
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
            using (var client = new AphiaNameServicePortTypeClient())
            {
                await client.OpenAsync().ConfigureAwait(false);

                var aphiaRecords = await client.getAphiaRecordsAsync(new getAphiaRecordsRequest(scientificName, false, true, false, 0)).ConfigureAwait(false);

                await client.CloseAsync().ConfigureAwait(false);

                return aphiaRecords;
            }
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
