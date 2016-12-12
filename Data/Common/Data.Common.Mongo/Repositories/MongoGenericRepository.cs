namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Repositories;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.Data.Common.Extensions;

    public class MongoGenericRepository<TEntity> : MongoCrudRepository<TEntity, TEntity>, IMongoGenericRepository<TEntity>
        where TEntity : class
    {
        public MongoGenericRepository(IMongoDatabaseProvider provider)
            : base(provider)
        {
        }

        public override async Task<object> Add(TEntity entity)
        {
            DummyValidator.ValidateEntity(entity);

            await this.Collection.InsertOneAsync(entity);
            return entity;
        }

        public override async Task<object> Update(TEntity entity)
        {
            DummyValidator.ValidateEntity(entity);

            var id = entity.GetIdValue<BsonIdAttribute>();
            var filter = this.GetFilterById(id);
            var result = await this.Collection.ReplaceOneAsync(filter, entity);
            return result;
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
