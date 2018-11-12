namespace ProcessingTools.Data.Mongo.Bio.Biorepositories
{
    using Abstractions;

    public class BiorepositoriesRepository<T> : MongoGenericRepository<T>, IBiorepositoriesRepository<T>
        where T : class
    {
        public BiorepositoriesRepository(IMongoDatabaseProvider provider)
            : base(provider)
        {
        }
    }
}
