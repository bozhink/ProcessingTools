namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System;
    using System.Threading.Tasks;

    using Contracts;
    using Mongo.Contracts;

    using MongoDB.Driver;

    using ProcessingTools.Common.Exceptions;

    public abstract class MongoCrudRepository<TEntity, TDbModel> : MongoRepository<TDbModel>, IMongoCrudRepository<TEntity>
        where TEntity : class
        where TDbModel : class, TEntity
    {
        public MongoCrudRepository(IMongoDatabaseProvider provider)
            : base(provider)
        {
        }

        public abstract Task<object> Add(TEntity entity);

        public virtual async Task<object> Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = this.GetFilterById(id);
            var result = await this.Collection.DeleteOneAsync(filter);
            return result;
        }

        public abstract Task<object> Delete(TEntity entity);

        public virtual async Task<TEntity> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = this.GetFilterById(id);
            var entity = await this.Collection
                .Find(filter)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new EntityNotFoundException();
            }

            return entity;
        }

        public virtual Task<long> SaveChanges() => Task.FromResult(0L);

        public abstract Task<object> Update(TEntity entity);
    }
}
