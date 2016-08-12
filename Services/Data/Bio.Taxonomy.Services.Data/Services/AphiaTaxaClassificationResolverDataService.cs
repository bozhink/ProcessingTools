namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Contracts;
    using Factories;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.Aphia;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Common.Constants;

    public class AphiaTaxaClassificationResolverDataService : TaxaInformationResolverDataServiceFactory<ITaxonClassification>, IAphiaTaxaClassificationResolverDataService
    {
        protected override void Delay()
        {
            Thread.Sleep(ConcurrencyConstants.DefaultDelayTime);
        }

        protected override Task ResolveScientificName(string scientificName, ConcurrentQueue<ITaxonClassification> taxaRanks)
        {
            return Task.Run(() =>
            {
                using (var aphiaService = new AphiaNameService())
                {
                    var aphiaRecords = aphiaService.getAphiaRecords(scientificName, false, true, false, 0);
                    if (aphiaRecords != null && aphiaRecords.Length > 0)
                    {
                        var records = new HashSet<ITaxonClassification>(aphiaRecords
                            .Where(s => string.Compare(s.scientificname, scientificName, true) == 0)
                            .Select(MapAphiaRecordToTaxonClassification));

                        foreach (var record in records)
                        {
                            taxaRanks.Enqueue(record);
                        }
                    }
                }
            });
        }

        private ITaxonClassification MapAphiaRecordToTaxonClassification(AphiaRecord record)
        {
            var result = new TaxonClassificationServiceModel
            {
                Rank = record.rank.MapTaxonRankStringToTaxonRankType(),
                ScientificName = record.scientificname,
                Authority = record.authority,
                CanonicalName = record.valid_name
            };

            result.Classification.Add(new TaxonRankServiceModel
            {
                Rank = TaxonRankType.Kingdom,
                ScientificName = record.kingdom
            });

            result.Classification.Add(new TaxonRankServiceModel
            {
                Rank = TaxonRankType.Phylum,
                ScientificName = record.phylum
            });

            result.Classification.Add(new TaxonRankServiceModel
            {
                Rank = TaxonRankType.Class,
                ScientificName = record.@class
            });

            result.Classification.Add(new TaxonRankServiceModel
            {
                Rank = TaxonRankType.Order,
                ScientificName = record.order
            });

            result.Classification.Add(new TaxonRankServiceModel
            {
                Rank = TaxonRankType.Family,
                ScientificName = record.family
            });

            result.Classification.Add(new TaxonRankServiceModel
            {
                Rank = TaxonRankType.Genus,
                ScientificName = record.genus
            });

            return result;
        }
    }
}
