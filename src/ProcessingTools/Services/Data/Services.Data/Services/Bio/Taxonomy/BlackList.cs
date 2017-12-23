namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Extensions.Linq;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Contracts.Data.Repositories.Bio.Taxonomy;
    using ProcessingTools.Contracts.Services.Data.Bio.Taxonomy;

    public class BlackList : IBlackList
    {
        private readonly IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider;

        public BlackList(IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider)
        {
            this.repositoryProvider = repositoryProvider ?? throw new ArgumentNullException(nameof(repositoryProvider));
        }

        public IEnumerable<string> GetItems()
        {
            return this.GetItemsAsync().Result;
        }

        public Task<IEnumerable<string>> GetItemsAsync()
        {
            return this.repositoryProvider.ExecuteAsync<IEnumerable<string>>((repository) =>
            {
                var result = repository.Entities
                    .Select(s => s.Content)
                    .ToList();

                return new HashSet<string>(result);
            });
        }
    }
}
