namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Contracts;
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

        public abstract Task<object> AddAsync(TEntity entity);

        public virtual Task<long> CountAsync() => this.Collection.CountAsync(m => true);

        public virtual Task<long> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            var result = this.Query.LongCount(filter);
            return Task.FromResult(result);
        }

        public virtual async Task<object> DeleteAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = this.GetFilterById(id);
            var result = await this.Collection.DeleteOneAsync(filter).ConfigureAwait(false);
            return result;
        }

        // TODO
        public virtual Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var query = this.Collection.AsQueryable().Where(filter).AsEnumerable();
            return Task.FromResult(query);
        }

        public virtual Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            // TODO
            var entity = this.Collection
                 .AsQueryable()
                 .FirstOrDefault(filter);
            return Task.FromResult(entity);
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = this.GetFilterById(id);
            var entity = await this.Collection.Find(filter).FirstOrDefaultAsync().ConfigureAwait(false);
            return entity;
        }

        public abstract Task<object> UpdateAsync(TEntity entity);

        public virtual async Task<object> UpdateAsync(object id, IUpdateExpression<TEntity> updateExpression)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (updateExpression == null)
            {
                throw new ArgumentNullException(nameof(updateExpression));
            }

            var updateQuery = this.ConvertUpdateExpressionToMongoUpdateQuery(updateExpression);
            var filter = this.GetFilterById(id);
            var result = await this.Collection.UpdateOneAsync(filter, updateQuery).ConfigureAwait(false);
            return result;
        }

        protected UpdateDefinition<TDbModel> ConvertUpdateExpressionToMongoUpdateQuery(IUpdateExpression<TEntity> updateExpression)
        {
            var updateCommands = updateExpression.UpdateCommands.ToArray();
            if (updateCommands.Length < 1)
            {
                throw new ArgumentNullException(nameof(updateExpression));
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
