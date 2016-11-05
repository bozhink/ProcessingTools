namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories
{
    using System;

    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.Contracts;
    using ProcessingTools.Contracts.Data.Repositories;

    public class BiorepositoriesRepositoryProvider<T> : IBiorepositoriesRepositoryProvider<T>
        where T : class
    {
        private readonly IBiorepositoriesMongoDatabaseProvider contextProvider;

        public BiorepositoriesRepositoryProvider(IBiorepositoriesMongoDatabaseProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public ISearchableCountableCrudRepository<T> Create()
        {
            return new BiorepositoriesRepository<T>(this.contextProvider);
        }
    }
}
