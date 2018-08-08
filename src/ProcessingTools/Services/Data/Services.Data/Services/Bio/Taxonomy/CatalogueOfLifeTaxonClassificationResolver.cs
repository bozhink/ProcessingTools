namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Clients.Contracts.Bio.Taxonomy;
    using ProcessingTools.Clients.Models.Bio.Taxonomy.CatalogueOfLife.Xml;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Extensions;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Abstractions.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    public class CatalogueOfLifeTaxonClassificationResolver : AbstractTaxonInformationResolver<ITaxonClassification>, ICatalogueOfLifeTaxonClassificationResolver
    {
        private readonly ICatalogueOfLifeDataRequester requester;

        public CatalogueOfLifeTaxonClassificationResolver(ICatalogueOfLifeDataRequester requester)
        {
            this.requester = requester ?? throw new ArgumentNullException(nameof(requester));
        }

        protected override async Task<ITaxonClassification[]> ResolveScientificNameAsync(string scientificName)
        {
            var response = await this.requester.RequestDataAsync(scientificName).ConfigureAwait(false);

            try
            {
                return response.Results
                    .Where(r => r.Name == scientificName)
                    .DefaultIfEmpty()
                    .Where(r => r != null)
                    .Select(this.MapResultToClassification)
                    .ToArray();
            }
            catch (ArgumentNullException)
            {
                // Linq queries failed. There are no matching response items.
            }

            return Array.Empty<ITaxonClassification>();
        }

        private ITaxonClassification MapResultToClassification(Result result)
        {
            var taxonClassification = new TaxonClassification
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

                return new TaxonRank
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
