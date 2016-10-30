namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Mongo.Contracts;

    using MongoDB.Driver;

    using ProcessingTools.Common.Validation;
    using ProcessingTools.Contracts.Expressions;

    public abstract class MongoCrudRepository<TDbModel, TEntity> : MongoSearchableRepository<TDbModel, TEntity>, IMongoCrudRepository<TEntity>
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
            DummyValidator.ValidateId(id);

            var filter = this.GetFilterById(id);
            var result = await this.Collection.DeleteOneAsync(filter);
            return result;
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
