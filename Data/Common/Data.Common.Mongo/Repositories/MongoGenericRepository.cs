namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Data.Common.Extensions;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Common.Mongo.Contracts.Repositories;

    public class MongoGenericRepository<T> : MongoCrudRepository<T, T>, IMongoGenericRepository<T>
        where T : class
    {
        public MongoGenericRepository(IMongoDatabaseProvider provider)
            : base(provider)
        {
        }

        public override async Task<object> Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await this.Collection.InsertOneAsync(entity);
            return entity;
        }

        public override async Task<object> Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var id = entity.GetIdValue<BsonIdAttribute>();
            var filter = this.GetFilterById(id);
            var result = await this.Collection.ReplaceOneAsync(filter, entity);
            return result;
        }

        public override async Task<long> Count()
        {
            var count = await this.Collection.CountAsync("{}");
            return count;
        }

        public override async Task<long> Count(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var count = await this.Collection.CountAsync(filter);
            return count;
        }
    }
}
