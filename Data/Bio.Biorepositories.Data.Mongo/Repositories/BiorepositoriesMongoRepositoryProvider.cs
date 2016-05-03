namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories
{
    using System;

    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public class BiorepositoriesMongoRepositoryProvider<T> : IBiorepositoriesMongoRepositoryProvider<T>
        where T : class
    {
        private readonly IBiorepositoriesMongoDatabaseProvider contextProvider;

        public BiorepositoriesMongoRepositoryProvider(IBiorepositoriesMongoDatabaseProvider contextProvider)
        {
            if (contextProvider == null)
            {
                throw new ArgumentNullException(nameof(contextProvider));
            }

            this.contextProvider = contextProvider;
        }

        public IGenericRepository<T> Create()
        {
            return new BiorepositoriesMongoRepository<T>(this.contextProvider);
        }
    }
}
