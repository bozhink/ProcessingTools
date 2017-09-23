namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Contracts;
    using ProcessingTools.Bio.Taxonomy.ServiceClient.CatalogueOfLife.Models;
    using ProcessingTools.Common.Extensions;
    using ProcessingTools.Constants;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Services.Data.Abstractions.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Data.Models.Bio.Taxonomy;

    public class CatalogueOfLifeTaxaClassificationResolver : AbstractTaxaInformationResolver<ITaxonClassification>, ICatalogueOfLifeTaxaClassificationResolver
    {
        private readonly ICatalogueOfLifeDataRequester requester;

        public CatalogueOfLifeTaxaClassificationResolver(ICatalogueOfLifeDataRequester requester)
        {
            this.requester = requester ?? throw new ArgumentNullException(nameof(requester));
        }

        protected override void Delay()
        {
            Thread.Sleep(ConcurrencyConstants.DefaultDelayTime);
        }

        protected override async Task<IEnumerable<ITaxonClassification>> ResolveScientificName(string scientificName)
        {
            var result = new HashSet<ITaxonClassification>();

            var response = await this.requester.RequestDataAsync(scientificName).ConfigureAwait(false);

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

        private ITaxonRank GetClassificationItem(AcceptedName result, TaxonRankType rank)
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
