namespace ProcessingTools.Bio.Taxonomy.Services.Data.Factories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ProcessingTools.Bio.Taxonomy.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;

    public abstract class TaxaRankResolverOverTaxaClassificationResolverDataServiceFactory : ITaxonRankResolverDataService
    {
        private readonly ITaxonClassificationResolverDataService service;

        public TaxaRankResolverOverTaxaClassificationResolverDataServiceFactory(ITaxonClassificationResolverDataService service)
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

            var response = await this.service.Resolve(scientificNames);

            return response.AsQueryable<ITaxonRank>();
        }
    }
}
