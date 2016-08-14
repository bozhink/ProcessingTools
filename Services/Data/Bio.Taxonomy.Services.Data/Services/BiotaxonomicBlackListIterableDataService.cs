namespace ProcessingTools.Bio.Taxonomy.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Repositories.Contracts;
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

        public async Task<IEnumerable<string>> All()
        {
            var repository = this.provider.Create();

            var query = await repository.All();

            var result = new HashSet<string>(query.Select(s => s.Content).ToList());

            repository.TryDispose();

            return result;
        }
    }
}
