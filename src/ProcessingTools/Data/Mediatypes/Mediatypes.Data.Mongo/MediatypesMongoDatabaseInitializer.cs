namespace ProcessingTools.Mediatypes.Data.Mongo
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Models;
    using MongoDB.Driver;
    using ProcessingTools.Data.Mongo;

    public class MediatypesMongoDatabaseInitializer : IMediatypesMongoDatabaseInitializer
    {
        private readonly IMongoDatabase db;

        public MediatypesMongoDatabaseInitializer(IMongoDatabaseProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.db = provider.Create();
        }

        public async Task<object> InitializeAsync()
        {
            await this.CreateIndicesToMediatypesCollection().ConfigureAwait(false);

            return true;
        }

        private async Task<object> CreateIndicesToMediatypesCollection()
        {
            string collectionName = MongoCollectionNameFactory.Create<Mediatype>();
            var collection = this.db.GetCollection<Mediatype>(collectionName);

            var result = await collection.Indexes
                .CreateOneAsync(new CreateIndexModel<Mediatype>(
                    Builders<Mediatype>.IndexKeys.Ascending(t => t.FileExtension)))
                .ConfigureAwait(false);

            return result;
        }
    }
}
