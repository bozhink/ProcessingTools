namespace ProcessingTools.Data.Common.Mongo.Repositories
{
    using System.Threading.Tasks;

    using MongoDB.Bson.Serialization.Attributes;

    using ProcessingTools.Common.Validation;
    using ProcessingTools.Data.Common.Extensions;
    using ProcessingTools.Data.Common.Mongo.Contracts;

    public class MongoCrudRepository<TEntity> : MongoCrudRepository<TEntity, TEntity>
        where TEntity : class
    {
        public MongoCrudRepository(IMongoDatabaseProvider provider)
            : base(provider)
        {
        }

        public override async Task<object> Add(TEntity entity)
        {
            DummyValidator.ValidateEntity(entity);

            await this.Collection.InsertOneAsync(entity);
            return entity;
        }

        public override async Task<object> Update(TEntity entity)
        {
            DummyValidator.ValidateEntity(entity);

            var id = entity.GetIdValue<BsonIdAttribute>();
            var filter = this.GetFilterById(id);
            var result = await this.Collection.ReplaceOneAsync(filter, entity);
            return result;
        }
    }
}
