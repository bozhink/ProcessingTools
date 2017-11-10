namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Constants;
    using ProcessingTools.Data.Contracts.Repositories;
    using ProcessingTools.Data.Contracts.Repositories.Bio.Taxonomy;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Data.Bio.Taxonomy;
    using ProcessingTools.Services.Models.Data.Bio.Taxonomy;

    public class TaxonRankSearchService : ITaxonRankSearchService
    {
        private readonly IGenericRepositoryProvider<ITaxonRanksRepository> repositoryProvider;

        public TaxonRankSearchService(IGenericRepositoryProvider<ITaxonRanksRepository> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        public async Task<IEnumerable<ITaxonRank>> Search(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return new ITaxonRank[] { };
            }

            return await this.repositoryProvider.Execute<IEnumerable<ITaxonRank>>(async (repository) =>
            {
                var searchString = filter.ToLowerInvariant();

                var data = await repository.FindAsync(t => t.Name.ToLower().Contains(searchString)).ConfigureAwait(false);
                var result = data.SelectMany(
                    t => t.Ranks.Select(rank => new TaxonRank
                    {
                        ScientificName = t.Name,
                        Rank = rank
                    }))
                    .Take(PaginationConstants.DefaultLargeNumberOfItemsPerPage)
                    .ToList();

                return result;
            })
            .ConfigureAwait(false);
        }
    }
}
