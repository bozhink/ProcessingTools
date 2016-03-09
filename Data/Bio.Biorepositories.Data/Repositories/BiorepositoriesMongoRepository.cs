namespace ProcessingTools.Bio.Biorepositories.Data.Repositories
{
    using Contracts;
    using ProcessingTools.Bio.Biorepositories.Data.Contracts;
    using ProcessingTools.Data.Common.Mongo.Repositories;

    public class BiorepositoriesMongoRepository<T> : MongoGenericRepository<T>, IBiorepositoriesMongoRepository<T>
        where T : class
    {
        public BiorepositoriesMongoRepository(IBiorepositoriesMongoDatabaseProvider provider)
            : base(provider)
        {
        }
    }
}