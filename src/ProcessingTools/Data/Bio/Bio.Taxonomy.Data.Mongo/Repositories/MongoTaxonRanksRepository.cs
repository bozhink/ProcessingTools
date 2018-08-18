namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories
{
    using System;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Common.Mongo.Repositories;
    using ProcessingTools.Data.Models.Bio.Taxonomy.Mongo;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;

    public class MongoTaxonRanksRepository : MongoCrudRepository<TaxonRankItem, ITaxonRankItem>, IMongoTaxonRankRepository
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

        public override Task<object> AddAsync(ITaxonRankItem entity)
        {
            return this.UpdateAsync(entity);
        }

        public override async Task<object> UpdateAsync(ITaxonRankItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = await this.Collection.UpdateOneAsync(
                Builders<TaxonRankItem>.Filter
                    .Eq(t => t.Name, entity.Name),
                Builders<TaxonRankItem>.Update
                    .Set(t => t.IsWhiteListed, entity.IsWhiteListed)
                    .AddToSetEach(t => t.Ranks, entity.Ranks),
                this.updateOptions)
                .ConfigureAwait(false);

            return result;
        }
    }
}
