namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Contracts.Repositories;
    using Models;
    using MongoDB.Driver;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Models;
    using ProcessingTools.Common.Validation;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Common.Mongo.Repositories;

    public class MongoTaxonRankRepository : MongoCrudRepository<MongoTaxonRankEntity, ITaxonRankEntity>, IMongoTaxonRankRepository
    {
        private readonly UpdateOptions updateOptions;

        public MongoTaxonRankRepository(IMongoDatabaseProvider provider)
            : base(provider)
        {
            this.updateOptions = new UpdateOptions
            {
                IsUpsert = true,
                BypassDocumentValidation = false
            };
        }

        public override Task<object> Add(ITaxonRankEntity entity)
        {
            return this.Update(entity);
        }

        public override async Task<long> Count()
        {
            var count = await this.Collection.CountAsync("{}");
            return count;
        }

        public override Task<long> Count(Expression<Func<ITaxonRankEntity, bool>> filter) => Task.Run(() =>
        {
            DummyValidator.ValidateFilter(filter);

            var count = this.Collection.AsQueryable()
                .Cast<ITaxonRankEntity>()
                .LongCount(filter);

            return count;
        });

        public override async Task<object> Update(ITaxonRankEntity entity)
        {
            DummyValidator.ValidateEntity(entity);

            var result = await this.Collection.UpdateOneAsync(
                Builders<MongoTaxonRankEntity>.Filter
                    .Eq(t => t.Name, entity.Name),
                Builders<MongoTaxonRankEntity>.Update
                    .Set(t => t.IsWhiteListed, entity.IsWhiteListed)
                    .AddToSetEach(t => t.Ranks, entity.Ranks),
                this.updateOptions);

            return result;
        }
    }
}
