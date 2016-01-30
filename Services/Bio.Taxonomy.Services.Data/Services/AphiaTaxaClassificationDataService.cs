namespace ProcessingTools.Bio.Taxonomy.Services.Data.Services
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Factories;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.Aphia;
    using ProcessingTools.Infrastructure.Concurrency;

    public class AphiaTaxaClassificationDataService : TaxaDataServiceFactory<ITaxonClassification>, IAphiaTaxaClassificationDataService
    {
        protected override void Delay()
        {
            Delayer.Delay();
        }

        protected override void ResolveScientificName(string scientificName, ConcurrentQueue<ITaxonClassification> taxaRanks)
        {
            using (var aphiaService = new AphiaNameService())
            {
                var aphiaRecords = aphiaService.getAphiaRecords(scientificName, false, true, false, 0);
                if (aphiaRecords != null && aphiaRecords.Length > 0)
                {
                    var records = new HashSet<TaxonClassificationDataServiceResponseModel>(aphiaRecords
                        .Where(s => string.Compare(s.scientificname, scientificName, true) == 0)
                        .Select(s => new TaxonClassificationDataServiceResponseModel
                        {
                            Kingdom = s.kingdom,
                            Phylum = s.phylum,
                            Class = s.@class,
                            Order = s.order,
                            Family = s.family,
                            Genus = s.genus,
                            Rank = s.rank?.ToLower(),
                            ScientificName = s.scientificname,
                            Authority = s.authority,
                            CanonicalName = s.valid_name
                        }));

                    foreach (var record in records)
                    {
                        taxaRanks.Enqueue(record);
                    }
                }
            }
        }
    }
}