namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Models;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Common.Mongo.Repositories;

    public class MongoTaxonRanksRepository : MongoCrudRepository<MongoTaxonRankEntity, ITaxonRankEntity>, IMongoTaxonRankRepository
    {
        private readonly UpdateOptions updateOptions;

        public MongoTaxonRanksRepository(IMongoDatabaseProvider provider)
            : base(provider)
        {
            this.updateOptions = new UpdateOptions
            {
                IsUpsert = true,
                BypassDocumentValidation = false
            };
        }

        public override Task<object> AddAsync(ITaxonRankEntity entity)
        {
            return this.UpdateAsync(entity);
        }

        public override async Task<long> CountAsync()
        {
            var count = await this.Collection.CountAsync("{}").ConfigureAwait(false);
            return count;
        }

        public override async Task<long> CountAsync(Expression<Func<ITaxonRankEntity, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return await Task.Run(() =>
            {
                var count = this.Collection.AsQueryable()
                    .Cast<ITaxonRankEntity>()
                    .LongCount(filter);

                return count;
            })
            .ConfigureAwait(false);
        }

        public override async Task<object> UpdateAsync(ITaxonRankEntity entity)
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
                this.updateOptions)
                .ConfigureAwait(false);

            return result;
        }
    }
}
