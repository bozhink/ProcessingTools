namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories
{
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.Contracts;
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
