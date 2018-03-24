namespace ProcessingTools.Mediatypes.Data.Mongo.Repositories
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Driver;
    using ProcessingTools.Data.Common.Mongo;
    using ProcessingTools.Data.Common.Mongo.Contracts;
    using ProcessingTools.Data.Contracts.Mediatypes;
    using ProcessingTools.Mediatypes.Data.Mongo.Models;
    using ProcessingTools.Models.Contracts.Mediatypes;

    public class MongoMediatypesSearchableRepository : ISearchableMediatypesRepository
    {
        private readonly IMongoCollection<Mediatype> collection;

        public MongoMediatypesSearchableRepository(IMongoDatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
            {
                throw new ArgumentNullException(nameof(databaseProvider));
            }

            string collectionName = MongoCollectionNameFactory.Create<Mediatype>();
            this.collection = databaseProvider.Create().GetCollection<Mediatype>(collectionName);
        }

        public IEnumerable<IMediatypeEntity> GetByFileExtension(string fileExtension)
        {
            var extension = fileExtension?.ToLower().Trim(' ', '.');
            var cursor = this.collection.Find(e => e.FileExtension.ToLower() == extension).ToCursor();
            return cursor.ToEnumerable();
        }
    }
}
