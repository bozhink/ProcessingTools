namespace ProcessingTools.Mediatypes.Data.Mongo
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using Models;
    using MongoDB.Driver;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Common.Mongo.Factories;

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

        public async Task<object> Initialize()
        {
            await this.CreateIndicesToMediatypesCollection();

            return true;
        }

        private async Task<object> CreateIndicesToMediatypesCollection()
        {
            string collectionName = CollectionNameFactory.Create<Mediatype>();
            var collection = this.db.GetCollection<Mediatype>(collectionName);

            var result = await collection.Indexes
                .CreateOneAsync(
                    Builders<Mediatype>.IndexKeys.Ascending(t => t.FileExtension));

            return result;
        }
    }
}
