namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Bio.Taxonomy;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Contracts.Data.Bio.Taxonomy.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;

    public class BlackListSearchService : IBlackListSearchService
    {
        private readonly IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider;

        public BlackListSearchService(IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        public async Task<IEnumerable<string>> Search(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return new string[] { };
            }

            return await this.repositoryProvider.Execute(async (repository) =>
            {
                var searchString = filter.ToLowerInvariant();

                var result = await repository.Entities
                    .Where(s => s.Content.ToLowerInvariant().Contains(searchString))
                    .Select(s => s.Content)
                    .ToListAsync()
                    .ConfigureAwait(false);

                return new HashSet<string>(result);
            })
            .ConfigureAwait(false);
        }
    }
}
