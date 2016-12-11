namespace ProcessingTools.Bio.Taxonomy.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Repositories;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Extensions.Linq;

    public class BiotaxonomicBlackListIterableDataService : IBiotaxonomicBlackListIterableDataService
    {
        private readonly IGenericRepositoryProvider<IBiotaxonomicBlackListIterableRepository> repositoryProvider;

        public BiotaxonomicBlackListIterableDataService(IGenericRepositoryProvider<IBiotaxonomicBlackListIterableRepository> repositoryProvider)
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
