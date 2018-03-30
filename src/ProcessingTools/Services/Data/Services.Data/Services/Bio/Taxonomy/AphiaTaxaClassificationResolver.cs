namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Clients.Bio.Aphia.ServiceReference;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Abstractions.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    public class AphiaTaxaClassificationResolver : AbstractTaxaInformationResolver<ITaxonClassification>, IAphiaTaxaClassificationResolver
    {
        protected override async Task<ITaxonClassification[]> ResolveScientificNameAsync(string scientificName)
        {
            var aphiaRecords = await this.GetAphiaRecords(scientificName);

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

        private async Task<getAphiaRecordsResponse> GetAphiaRecords(string scientificName)
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
                CanonicalName = record.valid_name
            };

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Kingdom,
                ScientificName = record.kingdom
            });

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Phylum,
                ScientificName = record.phylum
            });

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Class,
                ScientificName = record.@class
            });

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Order,
                ScientificName = record.order
            });

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Family,
                ScientificName = record.family
            });

            result.Classification.Add(new TaxonRank
            {
                Rank = TaxonRankType.Genus,
                ScientificName = record.genus
            });

            return result;
        }
    }
}
