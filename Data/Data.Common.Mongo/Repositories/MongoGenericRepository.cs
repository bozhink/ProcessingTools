namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    using ProcessingTools.Common.Validation;
    using ProcessingTools.Data.Common.Mongo.Contracts;

    public class MongoGenericRepository<TEntity> : MongoCrudRepository<TEntity>, IMongoGenericRepository<TEntity>
        where TEntity : class
    {
        public MongoGenericRepository(IMongoDatabaseProvider provider)
            : base(provider)
        {
        }

        public virtual async Task<long> Count()
        {
            var count = await this.Collection.CountAsync("{}");
            return count;
        }

        public virtual async Task<long> Count(Expression<Func<TEntity, bool>> filter)
        {
            DummyValidator.ValidateFilter(filter);

            var count = await this.Collection.CountAsync(filter);
            return count;
        }
    }
}
