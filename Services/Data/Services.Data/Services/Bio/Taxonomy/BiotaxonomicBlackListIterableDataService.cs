namespace ProcessingTools.Services.Data.Services.Bio.Taxonomy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Bio.Taxonomy;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Extensions.Linq;

    public class BiotaxonomicBlackListIterableDataService : IBiotaxonomicBlackListIterableDataService
    {
        private readonly IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider;

        public BiotaxonomicBlackListIterableDataService(IGenericRepositoryProvider<IBiotaxonomicBlackListRepository> repositoryProvider)
        {
            if (repositoryProvider == null)
            {
                throw new ArgumentNullException(nameof(repositoryProvider));
            }

            this.repositoryProvider = repositoryProvider;
        }

        public async Task<IEnumerable<string>> All() => await this.repositoryProvider.Execute(async (repository) =>
        {
            var result = await repository.Entities
                .Select(s => s.Content)
                .ToListAsync();

            return new HashSet<string>(result);
        });
    }
}
