namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;
    using Models;

    using MongoDB.Driver;

    using ProcessingTools.Bio.Taxonomy.Data.Common.Models.Contracts;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts;
    using ProcessingTools.Common.Exceptions;
    using ProcessingTools.Data.Common.Expressions.Contracts;

    public class MongoTaxonRankRepository : MongoTaxonRankSearchableRepository, IMongoTaxonRankRepository
    {
        private readonly UpdateOptions updateOptions;

        public MongoTaxonRankRepository(IBiotaxonomyMongoDatabaseProvider provider)
            : base(provider)
        {
            this.updateOptions = new UpdateOptions
            {
                IsUpsert = true,
                BypassDocumentValidation = false
            };
        }

        public virtual Task<object> Add(ITaxonRankEntity entity)
        {
            return this.Update(entity);
        }

        public virtual Task<IQueryable<ITaxonRankEntity>> All() => Task.FromResult(this.Collection.AsQueryable().Cast<ITaxonRankEntity>());

        public virtual async Task<long> Count()
        {
            var count = await this.Collection.CountAsync("{}");
            return count;
        }

        public virtual Task<long> Count(Expression<Func<ITaxonRankEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return Task.Run(() =>
            {
                var count = this.Collection.AsQueryable()
                    .Cast<ITaxonRankEntity>()
                    .LongCount(filter);

                return count;
            });
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

        public virtual async Task<object> Delete(ITaxonRankEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = await this.Collection.DeleteOneAsync(t => t.Name == entity.Name);

            return result;
        }

        public virtual async Task<ITaxonRankEntity> Get(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var filter = this.GetFilterById(id);
            var mongoEntity = await this.Collection
                .Find(filter)
                .FirstOrDefaultAsync();

            if (mongoEntity == null)
            {
                throw new EntityNotFoundException();
            }

            return mongoEntity;
        }

        public virtual Task<long> SaveChanges() => Task.FromResult(0L);

        public virtual async Task<object> Update(ITaxonRankEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = await this.Collection.UpdateOneAsync(
                Builders<MongoTaxonRankEntity>.Filter
                    .Eq(t => t.Name, entity.Name),
                Builders<MongoTaxonRankEntity>.Update
                    .Set(t => t.IsWhiteListed, entity.IsWhiteListed)
                    .AddToSetEach(t => t.Ranks, entity.Ranks),
                this.updateOptions);

            return result;
        }

        // TODO : repeated code
        public async Task<object> Update(object id, IUpdateExpression<ITaxonRankEntity> update)
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

        protected UpdateDefinition<MongoTaxonRankEntity> ConvertUpdateExpressionToMongoUpdateQuery(IUpdateExpression<ITaxonRankEntity> update)
        {
            var updateCommands = update.UpdateCommands.ToArray();
            if (updateCommands.Length < 1)
            {
                throw new ArgumentNullException(nameof(update.UpdateCommands));
            }

            var updateCommand = updateCommands[0];
            var updateQuery = Builders<MongoTaxonRankEntity>.Update
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
