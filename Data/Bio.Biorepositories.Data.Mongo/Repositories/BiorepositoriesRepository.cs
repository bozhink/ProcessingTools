namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories
{
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Contracts;
    using ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories.Contracts;
    using ProcessingTools.Data.Common.Mongo.Repositories;

    public class BiorepositoriesRepository<T> : MongoGenericRepository<T>, IBiorepositoriesRepository<T>
        where T : class
    {
        public BiorepositoriesRepository(IBiorepositoriesMongoDatabaseProvider provider)
            : base(provider)
        {
        }
    }
}
