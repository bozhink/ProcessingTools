namespace ProcessingTools.Mediatypes.Data.Mongo.Repositories
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Driver;
    using ProcessingTools.Contracts.Data.Mediatypes.Models;
    using ProcessingTools.Contracts.Data.Mediatypes.Repositories;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Common.Mongo.Factories;
    using ProcessingTools.Mediatypes.Data.Mongo.Models;

    public class MongoMediatypesSearchableRepository : ISearchableMediatypesRepository
    {
        private readonly IMongoCollection<Mediatype> collection;

        public MongoMediatypesSearchableRepository(IMongoDatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
            {
                throw new ArgumentNullException(nameof(databaseProvider));
            }

            string collectionName = CollectionNameFactory.Create<Mediatype>();
            this.collection = databaseProvider.Create().GetCollection<Mediatype>(collectionName);
        }

        public IEnumerable<IMediatype> GetByFileExtension(string fileExtension)
        {
            var extension = fileExtension?.ToLower().Trim(' ', '.');
            var cursor = this.collection.Find(e => e.FileExtension.ToLower() == extension).ToCursor();
            return cursor.ToEnumerable();
        }
    }
}
