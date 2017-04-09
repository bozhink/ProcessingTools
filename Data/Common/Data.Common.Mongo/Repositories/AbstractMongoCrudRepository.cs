namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Repositories;
    using MongoDB.Driver;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.Contracts.Expressions;

    public abstract class MongoCrudRepository<TDbModel, TEntity> : MongoRepository<TDbModel>, IMongoCrudRepository<TEntity>, IMongoSearchableRepository<TEntity>
        where TEntity : class
        where TDbModel : class, TEntity
    {
        public MongoCrudRepository(IMongoDatabaseProvider provider)
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
            DummyValidator.ValidateId(id);

            var filter = this.GetFilterById(id);
            var result = await this.Collection.DeleteOneAsync(filter);
            return result;
        }

        // TODO
        public virtual Task<IEnumerable<TEntity>> Find(
            Expression<Func<TEntity, bool>> filter) => Task.Run(() =>
            {
                DummyValidator.ValidateFilter(filter);

                var query = this.Collection.AsQueryable().Where(filter).AsEnumerable();
                return query;
            });

        public virtual Task<TEntity> FindFirst(
            Expression<Func<TEntity, bool>> filter) => Task.Run(() =>
            {
                DummyValidator.ValidateFilter(filter);

                var entity = this.Collection
                    .AsQueryable()
                    .FirstOrDefault(filter);
                return entity;
            });

        public async Task<TEntity> GetById(object id)
        {
            DummyValidator.ValidateId(id);

            var filter = this.GetFilterById(id);
            var entity = await this.Collection.Find(filter).FirstOrDefaultAsync();
            return entity;
        }

        public abstract Task<object> Update(TEntity entity);

        public virtual async Task<object> Update(object id, IUpdateExpression<TEntity> update)
        {
            DummyValidator.ValidateId(id);
            DummyValidator.ValidateUpdate(update);

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
                throw new ArgumentNullException(nameof(update.UpdateCommands));
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
