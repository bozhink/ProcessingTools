namespace ProcessingTools.Bio.Taxonomy.Services.Data.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Extensions;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Contracts;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Models;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Factories;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Models;
    using ProcessingTools.Bio.Taxonomy.Types;
    using ProcessingTools.Constants;

    public class CatalogueOfLifeTaxaClassificationResolverDataService : TaxaInformationResolverDataServiceFactory<ITaxonClassification>, ICatalogueOfLifeTaxaClassificationResolverDataService
    {
        private ICatalogueOfLifeDataRequester requester;

        public CatalogueOfLifeTaxaClassificationResolverDataService(ICatalogueOfLifeDataRequester requester)
        {
            if (requester == null)
            {
                throw new ArgumentNullException(nameof(requester));
            }

            this.requester = requester;
        }

        protected override void Delay()
        {
            Thread.Sleep(ConcurrencyConstants.DefaultDelayTime);
        }

        protected override async Task ResolveScientificName(string scientificName, ConcurrentQueue<ITaxonClassification> taxaQueue)
        {
            var response = await this.requester.RequestData(scientificName);

            try
            {
                var matchingResults = new HashSet<Result>(response.Results
                    .Where(r => r.Name == scientificName));

                matchingResults
                    .Select(this.MapResultToClassification)
                    .ToList()
                    .ForEach(r => taxaQueue.Enqueue(r));
            }
            catch (ArgumentNullException)
            {
                // Linq queries failed. There are no matching response items.
            }
        }

        private ITaxonClassification MapResultToClassification(Result result)
        {
            var classification = new TaxonClassificationServiceModel
            {
                ScientificName = result.Name,
                Rank = result.Rank.MapTaxonRankStringToTaxonRankType(),
                Authority = result.Author,
                CanonicalName = result.AcceptedName?.Name
            };

            foreach (var rank in Enum.GetValues(typeof(TaxonRankType)).Cast<TaxonRankType>())
            {
                var taxon = this.GetClassificationItem(result, rank);
                if (taxon != null)
                {
                    classification.Classification.Add(taxon);
                }
            }

            return classification;
        }

        private ITaxonRank GetClassificationItem(Result result, TaxonRankType rank)
        {
            try
            {
                var name = result.Classification
                    .FirstOrDefault(c => string.Compare(c.Rank, rank.MapTaxonRankTypeToTaxonRankString(), true) == 0)
                    .Name;

                return new TaxonRankServiceModel
                {
                    ScientificName = name,
                    Rank = rank
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
