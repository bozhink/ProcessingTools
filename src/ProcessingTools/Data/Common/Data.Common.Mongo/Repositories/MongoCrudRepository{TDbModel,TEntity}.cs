namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Contracts.Expressions;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Common.Mongo.Contracts.Repositories;

    public abstract class MongoCrudRepository<TDbModel, TEntity> : MongoRepository<TDbModel>, IMongoCrudRepository<TEntity>, IMongoSearchableRepository<TEntity>
        where TEntity : class
        where TDbModel : class, TEntity
    {
        protected MongoCrudRepository(IMongoDatabaseProvider provider)
            : base(provider)
        {
        }

        public virtual IQueryable<TEntity> Query => this.Collection.AsQueryable().AsQueryable<TEntity>();

        public abstract Task<object> Add(TEntity entity);

        public virtual async Task<long> Count()
        {
            var result = await this.Collection.CountAsync(m => true);
            return result;
        }

        public virtual Task<long> Count(Expression<Func<TEntity, bool>> filter)
        {
            var result = this.Query.LongCount(filter);
            return Task.FromResult(result);
        }

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

        // TODO
        public virtual async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = this.Collection.AsQueryable().Where(filter).AsEnumerable();
            return await Task.FromResult(query);
        }

        public virtual async Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            // TODO
            var entity = this.Collection
                 .AsQueryable()
                 .FirstOrDefault(filter);
            return await Task.FromResult(entity);
        }

        public async Task<TEntity> GetById(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = this.GetFilterById(id);
            var entity = await this.Collection.Find(filter).FirstOrDefaultAsync();
            return entity;
        }

        public abstract Task<object> Update(TEntity entity);

        public virtual async Task<object> Update(object id, IUpdateExpression<TEntity> update)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (update == null)
            {
                throw new ArgumentNullException(nameof(update));
            }

            var updateQuery = this.ConvertUpdateExpressionToMongoUpdateQuery(update);
            var filter = this.GetFilterById(id);
            var result = await this.Collection.UpdateOneAsync(filter, updateQuery);
            return result;
        }

        protected UpdateDefinition<TDbModel> ConvertUpdateExpressionToMongoUpdateQuery(IUpdateExpression<TEntity> update)
        {
            var updateCommands = update.UpdateCommands.ToArray();
            if (updateCommands.Length < 1)
            {
                throw new ArgumentNullException(nameof(update));
            }

            var updateCommand = updateCommands[0];
            var updateQuery = Builders<TDbModel>.Update
                .Set(updateCommand.FieldName, updateCommand.Value);
            for (int i = 1; i < updateCommands.Length; ++i)
            {
                updateCommand = updateCommands[i];
                updateQuery = updateQuery.Set(updateCommand.FieldName, updateCommand.Value);
            }

            return updateQuery;
        }
    }
}
