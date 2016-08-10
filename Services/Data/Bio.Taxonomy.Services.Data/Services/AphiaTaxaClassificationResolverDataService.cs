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
    using ProcessingTools.Bio.Taxonomy.ServiceClient.Aphia;
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
                        var records = new HashSet<TaxonClassificationServiceModel>(aphiaRecords
                            .Where(s => string.Compare(s.scientificname, scientificName, true) == 0)
                            .Select(s => new TaxonClassificationServiceModel
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
            });
        }
    }
}