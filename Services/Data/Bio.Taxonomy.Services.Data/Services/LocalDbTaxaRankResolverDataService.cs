namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Contracts;

    public class LocalDbTaxaRankResolverDataService : ILocalDbTaxaRankResolverDataService
    {
        private readonly ITaxonRankDataService service;

        public LocalDbTaxaRankResolverDataService(ITaxonRankDataService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            this.service = service;
        }

        public async Task<IQueryable<ITaxonRank>> Resolve(params string[] scientificNames)
        {
            if (scientificNames == null || scientificNames.Length < 1)
            {
                throw new ArgumentNullException(nameof(scientificNames));
            }

            return (await this.service.All())
                .Where(t => scientificNames.Contains(
                    t.ScientificName,
                    StringComparer.InvariantCultureIgnoreCase));
        }
    }
}