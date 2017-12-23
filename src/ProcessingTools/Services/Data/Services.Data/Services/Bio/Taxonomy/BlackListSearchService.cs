namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Data.Repositories.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Data.Bio.Taxonomy;

    public class BlackListSearchService : IBlackListSearchService
    {
        private readonly IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider;

        public BlackListSearchService(IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        public Task<string[]> SearchAsync(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return Task.FromResult(new string[] { });
            }

            return this.repositoryProvider.ExecuteAsync((repository) =>
            {
                var searchString = filter.ToUpperInvariant();

                return repository.Entities
                    .Where(s => s.Content.ToUpperInvariant().Contains(searchString))
                    .Select(s => s.Content)
                    .Distinct()
                    .ToArray();
            });
        }
    }
}
