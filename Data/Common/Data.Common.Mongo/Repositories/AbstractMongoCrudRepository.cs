namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts;
    using Contracts.Repositories;
    using MongoDB.Driver;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.Constants;
    using ProcessingTools.Contracts.Expressions;
    using ProcessingTools.Enumerations;

    public abstract class MongoCrudRepository<TDbModel, TEntity> : MongoRepository<TDbModel>, IMongoCrudRepository<TEntity>, IMongoSearchableRepository<TEntity>
        where TEntity : class
        where TDbModel : class, TEntity
    {
        public MongoCrudRepository(IMongoDatabaseProvider provider)
            : base(provider)
        {
        }

        public abstract Task<object> Add(TEntity entity);

        public Task<IQueryable<TEntity>> All() => Task.Run(() =>
        {
            var query = this.Collection.AsQueryable().AsQueryable<TEntity>();
            return query;
        });

        public virtual async Task<object> Delete(object id)
        {
            DummyValidator.ValidateId(id);

            var filter = this.GetFilterById(id);
            var result = await this.Collection.DeleteOneAsync(filter);
            return result;
        }

        public virtual Task<IQueryable<TEntity>> Find(
            Expression<Func<TEntity, bool>> filter) => Task.Run(() =>
            {
                DummyValidator.ValidateFilter(filter);

                var query = this.Collection.AsQueryable().Where(filter);
                return query;
            });

        public virtual Task<IQueryable<TEntity>> Find(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> sort,
            SortOrder sortOrder = SortOrder.Ascending,
            int skip = 0,
            int take = PagingConstants.DefaultNumberOfTopItemsToSelect) => Task.Run(() =>
            {
                DummyValidator.ValidateFilter(filter);
                DummyValidator.ValidateSort(sort);
                DummyValidator.ValidateSkip(skip);
                DummyValidator.ValidateTake(take);

                var query = this.Collection.AsQueryable().Where(filter);

                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        query = query.OrderBy(sort);
                        break;

                    case SortOrder.Descending:
                        query = query.OrderByDescending(sort);
                        break;

                    default:
                        throw new NotImplementedException();
                }

                query = query.Skip(skip).Take(take);
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

        public virtual Task<Tout> FindFirst<Tout>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, Tout>> projection) => Task.Run(() =>
            {
                DummyValidator.ValidateFilter(filter);
                DummyValidator.ValidateProjection(projection);
                var entity = this.Collection
                   .AsQueryable()
                   .Where(filter)
                   .Select(projection)
                   .FirstOrDefault();
                return entity;
            });

        public async Task<TEntity> Get(object id)
        {
            DummyValidator.ValidateId(id);

            var filter = this.GetFilterById(id);
            var entity = await this.Collection.Find(filter).FirstOrDefaultAsync();
            return entity;
        }

        public virtual Task<long> SaveChanges() => Task.FromResult(0L);

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
