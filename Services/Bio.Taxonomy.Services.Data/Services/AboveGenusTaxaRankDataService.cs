namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Contracts;

    public class AboveGenusTaxaRankDataService : IAboveGenusTaxaRankDataService
    {
        private const string Rank = "above-genus";

        public IQueryable<ITaxonClassification> Resolve(params string[] scientificNames)
        {
            var result = new HashSet<ITaxonClassification>(scientificNames
                .Select(s => new TaxonClassificationDataServiceResponseModel
                {
                    ScientificName = s,
                    Rank = Rank
                }));

            return result.AsQueryable();
        }
    }
}