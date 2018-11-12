namespace ProcessingTools.Data.Mongo.Bio.Biorepositories
{
    using Common.Contracts;
    using Common.Repositories;

    public class BiorepositoriesRepository<T> : MongoGenericRepository<T>, IBiorepositoriesRepository<T>
        where T : class
    {
        public BiorepositoriesRepository(IMongoDatabaseProvider provider)
            : base(provider)
        {
        }
    }
}
