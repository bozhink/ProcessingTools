namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MongoDB.Bson.Serialization.Attributes;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Common.Mongo.Contracts.Repositories;
    using ProcessingTools.Extensions.Data;

    public class MongoGenericRepository<T> : MongoCrudRepository<T, T>, IMongoGenericRepository<T>
        where T : class
    {
        public MongoGenericRepository(IMongoDatabaseProvider provider)
            : base(provider)
        {
        }

        public override async Task<object> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await this.Collection.InsertOneAsync(entity).ConfigureAwait(false);
            return entity;
        }

        public override async Task<object> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var id = entity.GetIdValue<BsonIdAttribute>();
            var filter = this.GetFilterById(id);
            var result = await this.Collection.ReplaceOneAsync(filter, entity).ConfigureAwait(false);
            return result;
        }

        public override Task<long> CountAsync()
        {
            return this.Collection.CountAsync("{}");
        }

        public override Task<long> CountAsync(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return this.Collection.CountAsync(filter);
        }
    }
}
