namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Data.Contracts.Repositories;
    using ProcessingTools.Data.Contracts.Repositories.Bio.Taxonomy;
    using ProcessingTools.Services.Contracts.Data.Bio.Taxonomy;

    public class BlackListSearchService : IBlackListSearchService
    {
        private readonly IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider;

        public BlackListSearchService(IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        public Task<IEnumerable<string>> Search(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return Task.FromResult<IEnumerable<string>>(new string[] { });
            }

            return this.repositoryProvider.Execute<IEnumerable<string>>(async (repository) =>
            {
                var searchString = filter.ToLowerInvariant();

                var result = await repository.Entities
                    .Where(s => s.Content.ToLowerInvariant().Contains(searchString))
                    .Select(s => s.Content)
                    .ToListAsync()
                    .ConfigureAwait(false);

                return new HashSet<string>(result);
            });
        }
    }
}
