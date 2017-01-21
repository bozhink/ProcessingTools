namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Abstractions.Bio.Taxonomy;
    using Contracts.Bio.Taxonomy;
    using Models.Bio.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Contracts;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Models;
    using ProcessingTools.Constants;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;

    public class CatalogueOfLifeTaxaClassificationResolver : AbstractTaxaInformationResolver<ITaxonClassification>, ICatalogueOfLifeTaxaClassificationResolver
    {
        private readonly ICatalogueOfLifeDataRequester requester;

        public CatalogueOfLifeTaxaClassificationResolver(ICatalogueOfLifeDataRequester requester)
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

        protected override async Task<IEnumerable<ITaxonClassification>> ResolveScientificName(string scientificName)
        {
            var result = new HashSet<ITaxonClassification>();

            var response = await this.requester.RequestData(scientificName);

            try
            {
                var matchingResults = new HashSet<Result>(response.Results
                    .Where(r => r.Name == scientificName));

                matchingResults
                    .Select(this.MapResultToClassification)
                    .ToList()
                    .ForEach(r => result.Add(r));
            }
            catch (ArgumentNullException)
            {
                // Linq queries failed. There are no matching response items.
            }

            return result;
        }

        private ITaxonClassification MapResultToClassification(Result result)
        {
            var taxonClassification = new TaxonClassificationServiceModel
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
                    taxonClassification.Classification.Add(taxon);
                }
            }

            return taxonClassification;
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
