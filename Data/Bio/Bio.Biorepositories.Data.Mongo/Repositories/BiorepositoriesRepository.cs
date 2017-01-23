namespace ProcessingTools.Bio.Biorepositories.Data.Mongo.Repositories
{
    using Contracts.Repositories;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Common.Mongo.Repositories;

    public class BiorepositoriesRepository<T> : MongoGenericRepository<T>, IBiorepositoriesRepository<T>
        where T : class
    {
        public BiorepositoriesRepository(IMongoDatabaseProvider provider)
            : base(provider)
        {
        }
    }
}
