namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.Aphia;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Abstractions.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    public class AphiaTaxaClassificationResolver : AbstractTaxaInformationResolver<ITaxonClassification>, IAphiaTaxaClassificationResolver
    {
        protected override Task<ITaxonClassification[]> ResolveScientificNameAsync(string scientificName)
        {
            return Task.Run(() =>
            {
                var result = new HashSet<ITaxonClassification>();

                using (var aphiaService = new AphiaNameService())
                {
                    var aphiaRecords = aphiaService.getAphiaRecords(scientificName, false, true, false, 0);
                    if (aphiaRecords != null && aphiaRecords.Length > 0)
                    {
                        var records = aphiaRecords
                            .Where(s => string.Compare(s.scientificname, scientificName, true) == 0)
                            .Select(this.MapAphiaRecordToTaxonClassification);

                        foreach (var record in records)
                        {
                            result.Add(record);
                        }
                    }
                }

                return result.ToArray();
            });
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
