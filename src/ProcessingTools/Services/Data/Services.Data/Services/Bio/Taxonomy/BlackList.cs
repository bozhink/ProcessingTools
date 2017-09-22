namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Common.Extensions.Linq;
    using ProcessingTools.Contracts.Data.Bio.Taxonomy.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Services.Data.Contracts.Bio.Taxonomy;

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
            return this.repositoryProvider.Execute<IEnumerable<string>>(async (repository) =>
            {
                var result = await repository.Entities
                    .Select(s => s.Content)
                    .ToListAsync()
                    .ConfigureAwait(false);

                return new HashSet<string>(result);
            });
        }
    }
}
