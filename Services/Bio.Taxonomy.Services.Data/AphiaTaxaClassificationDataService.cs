namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    using Infrastructure.Concurrency;
    using Models;
    using ServiceClient.Aphia;
    using Taxonomy.Contracts;

    public class AphiaTaxaClassificationDataService : TaxaDataService<ITaxonClassification>
    {
        protected override void Delay()
        {
            Delayer.Delay();
        }

        protected override void ResolveRank(string scientificName, ConcurrentQueue<ITaxonClassification> taxaRanks)
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
                            Rank = s.rank.ToLower(),
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