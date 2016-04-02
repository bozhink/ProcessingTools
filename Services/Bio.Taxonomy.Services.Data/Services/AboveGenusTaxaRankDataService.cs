namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using ProcessingTools.Bio.Taxonomy.Contracts;

    public class AboveGenusTaxaRankDataService : IAboveGenusTaxaRankDataService
    {
        private const string Rank = "above-genus";

        public Task<IQueryable<ITaxonClassification>> Resolve(params string[] scientificNames)
        {
            return Task.Run(() =>
            {
                var result = new HashSet<ITaxonClassification>(scientificNames
                    .Select(s => new TaxonClassificationServiceModel
                    {
                        ScientificName = s,
                        Rank = Rank
                    }));

                return result.AsQueryable();
            });
        }
    }
}