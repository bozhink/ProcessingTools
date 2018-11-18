namespace ProcessingTools.Mediatypes.Data.Mongo.Repositories
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Driver;
    using ProcessingTools.Data.Contracts.Files;
    using ProcessingTools.Data.Models.Mongo.Files;
    using ProcessingTools.Data.Mongo;
    using ProcessingTools.Models.Contracts.Files.Mediatypes;

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

        public IEnumerable<IMediatypeBaseModel> GetByFileExtension(string fileExtension)
        {
            var extension = fileExtension?.ToLower().Trim(' ', '.');
            var cursor = this.collection.Find(e => e.Extension.ToLower() == extension).ToCursor();
            return cursor.ToEnumerable();
        }
    }
}
