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
                    .Set(t => t.Ranks, entity.Ranks),
                this.updateOptions);

            return result;
        }
    }
}
