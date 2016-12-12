namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using Contracts;

    public class MongoSearchableRepository<TEntity> : MongoSearchableRepository<TEntity, TEntity>
        where TEntity : class
    {
        public MongoSearchableRepository(IMongoDatabaseProvider provider)
            : base(provider)
        {
        }
    }
}
