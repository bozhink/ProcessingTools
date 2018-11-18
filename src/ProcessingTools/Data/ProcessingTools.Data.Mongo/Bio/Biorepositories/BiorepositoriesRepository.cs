namespace ProcessingTools.Data.Mongo.Bio.Biorepositories
{
    using ProcessingTools.Data.Mongo.Abstractions;

    public class BiorepositoriesRepository<T> : MongoGenericRepository<T>, IBiorepositoriesRepository<T>
        where T : class
    {
        public BiorepositoriesRepository(IMongoDatabaseProvider provider)
            : base(provider)
        {
        }
    }
}
