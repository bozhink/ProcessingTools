namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories
{
    using System;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Contracts.Data.Repositories;
    using ProcessingTools.Data.Common.Mongo.Contracts;

    public class BiorepositoriesRepositoryProvider<T> : IBiorepositoriesRepositoryProvider<T>
        where T : class
    {
        private readonly IMongoDatabaseProvider contextProvider;

        public BiorepositoriesRepositoryProvider(IMongoDatabaseProvider contextProvider)
        {
            this.contextProvider = contextProvider ?? throw new ArgumentNullException(nameof(contextProvider));
        }

        public ICrudRepository<T> Create()
        {
            return new BiorepositoriesRepository<T>(this.contextProvider);
        }
    }
}
