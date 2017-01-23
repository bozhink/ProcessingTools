namespace ProcessingTools.Bio.Taxonomy.Data.Mongo.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.Repositories;
    using Models;
    using MongoDB.Driver;
    using ProcessingTools.Bio.Taxonomy.Data.Common.Contracts.Models;
    using ProcessingTools.Common.Validation;
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

        public override Task<object> Add(IBlackListEntity entity) => this.Update(entity);

        public override async Task<object> Update(IBlackListEntity entity)
        {
            DummyValidator.ValidateEntity(entity);

            var result = await this.Collection.UpdateOneAsync(
                Builders<MongoBlackListEntity>.Filter
                    .Eq(t => t.Content, entity.Content),
                Builders<MongoBlackListEntity>.Update
                    .Set(t => t.Content, entity.Content),
                this.updateOptions);

            return result;
        }
    }
}
