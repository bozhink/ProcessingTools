namespace ProcessingTools.Bio.Taxonomy.Services.Data.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Repositories.Contracts;
    using ProcessingTools.Bio.Taxonomy.Services.Data.Contracts;
    using ProcessingTools.Extensions;

    public class BiotaxonomicBlackListIterableDataService : IBiotaxonomicBlackListIterableDataService
    {
        private readonly IBiotaxonomicBlackListIterableRepositoryProvider provider;

        public BiotaxonomicBlackListIterableDataService(IBiotaxonomicBlackListIterableRepositoryProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.provider = provider;
        }

        public Task<IEnumerable<string>> All() => Task.Run(() =>
        {
            var repository = this.provider.Create();

            var result = new HashSet<string>(repository.Entities
                .Select(s => s.Content)
                .ToList());

            repository.TryDispose();

            return result.AsEnumerable();
        });
    }
}
