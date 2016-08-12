namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using ProcessingTools.Bio.Taxonomy.Contracts;

    public class AphiaTaxaRankResolverDataService : IAphiaTaxaRankResolverDataService
    {
        private readonly IAphiaTaxaClassificationResolverDataService service;

        public AphiaTaxaRankResolverDataService(IAphiaTaxaClassificationResolverDataService service)
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
