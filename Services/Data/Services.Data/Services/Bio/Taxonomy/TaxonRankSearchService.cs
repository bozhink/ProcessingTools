namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Bio.Taxonomy;
    using Models.Bio.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Models.Bio.Taxonomy;

    public class TaxonRankSearchService : ITaxonRankSearchService
    {
        private readonly IGenericRepositoryProvider<ITaxonRankRepository> repositoryProvider;

        public TaxonRankSearchService(IGenericRepositoryProvider<ITaxonRankRepository> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
        }

        public async Task<IEnumerable<ITaxonRank>> Search(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return new ITaxonRank[] { };
            }

            return await this.repositoryProvider.Execute(async (repository) =>
            {
                var searchString = filter.ToLower();

                var query = await repository.Find(t => t.Name.ToLower().Contains(searchString));

                var result = query.ToList()
                    .SelectMany(t => t.Ranks.Select(rank => new TaxonRankServiceModel
                    {
                        ScientificName = t.Name,
                        Rank = rank
                    }))
                    .Take(PagingConstants.DefaultLargeNumberOfItemsPerPage)
                    .ToList();

                return result;
            });
        }
    }
}
