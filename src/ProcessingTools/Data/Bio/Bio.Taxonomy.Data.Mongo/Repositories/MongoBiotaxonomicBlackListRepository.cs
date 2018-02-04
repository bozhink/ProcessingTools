namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Contracts.Repositories;
    using ProcessingTools.Bio.Taxonomy.Data.Mongo.Models;
    using ProcessingTools.Models.Contracts.Bio.Taxonomy;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Common.Mongo.Repositories;

    public class MongoBiotaxonomicBlackListRepository : MongoCrudRepository<MongoBlackListEntity, IBlackListEntity>, IMongoBiotaxonomicBlackListRepository
    {
        private readonly UpdateOptions updateOptions;

        public MongoBiotaxonomicBlackListRepository(IMongoDatabaseProvider provider)
            : base(provider)
        {
            this.updateOptions = new UpdateOptions
            {
                IsUpsert = true,
                BypassDocumentValidation = false
            };
        }

        public IEnumerable<IBlackListEntity> Entities => this.Collection
           .Find("{}")
           .ToCursor()
           .ToEnumerable<IBlackListEntity>();

        public override Task<object> AddAsync(IBlackListEntity entity) => this.UpdateAsync(entity);

        public override async Task<object> UpdateAsync(IBlackListEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var result = await this.Collection.UpdateOneAsync(
                Builders<MongoBlackListEntity>.Filter
                    .Eq(t => t.Content, entity.Content),
                Builders<MongoBlackListEntity>.Update
                    .Set(t => t.Content, entity.Content),
                this.updateOptions)
                .ConfigureAwait(false);

            return result;
        }
    }
}
