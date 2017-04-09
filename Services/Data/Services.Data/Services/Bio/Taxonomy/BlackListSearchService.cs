namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Bio.Taxonomy;
    using ProcessingTools.Contracts.Data.Bio.Taxonomy.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Extensions.Linq;

    public class BlackListSearchService : IBlackListSearchService
    {
        private readonly IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider;

        public BlackListSearchService(IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
        }

        public async Task<IEnumerable<string>> Search(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return new string[] { };
            }

            return await this.repositoryProvider.Execute(async (repository) =>
            {
                var searchString = filter.ToLower();

                var result = await repository.Entities
                    .Where(s => s.Content.ToLower().Contains(searchString))
                    .Select(s => s.Content)
                    .ToListAsync();

                return new HashSet<string>(result);
            });
        }
    }
}
