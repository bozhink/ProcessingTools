namespace ProcessingTools.Mediatypes.Data.Seed.Seeders
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using Newtonsoft.Json;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Data.Models.Mongo.Files;
    using ProcessingTools.Data.Mongo;
    using ProcessingTools.Mediatypes.Data.Seed.Contracts;
    using ProcessingTools.Mediatypes.Data.Seed.Models;

    public class MediatypesMongoDatabaseSeeder : IMediatypesMongoDatabaseSeeder
    {
        private readonly IMongoDatabase db;

        private ConcurrentQueue<Exception> exceptions;

        public MediatypesMongoDatabaseSeeder(IMongoDatabaseProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.db = provider.Create();
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        public async Task<object> SeedAsync()
        {
            this.exceptions = new ConcurrentQueue<Exception>();

            ExtensionJson[] mediatypesJson = this.ParseDataJsonFile();

            if (mediatypesJson.Length < 1)
            {
                throw new ProcessingTools.Common.Exceptions.InvalidDataException("Mediatypes data json file is empty or invalid.");
            }

            await this.ImportMediatypesToDatabase(mediatypesJson).ConfigureAwait(false);

            if (this.exceptions.Count > 0)
            {
                throw new AggregateException(this.exceptions);
            }

            return true;
        }

        private ExtensionJson[] ParseDataJsonFile()
        {
            string jsonFilePath = AppSettings.MediaTypeDataJsonFileName;

            string jsonString = File.ReadAllText(jsonFilePath);

            var mediatypesJson = JsonConvert.DeserializeObject<ExtensionJson[]>(jsonString);
            return mediatypesJson?.Select(m => new ExtensionJson
            {
                Extension = m.Extension.ToLowerInvariant(),
                Mimetype = m.Mimetype.ToLowerInvariant(),
                Mimesubtype = m.Mimesubtype.ToLowerInvariant(),
                Description = m.Description
            })
            .ToArray();
        }

        private async Task ImportMediatypesToDatabase(ExtensionJson[] mediatypes)
        {
            //// TODO string collectionName = MongoCollectionNameFactory.Create<Mediatype>();
            var collection = this.db.GetCollection<Mediatype>("mediatypes");

            foreach (var mediatype in mediatypes)
            {
                try
                {
                    await collection.InsertOneAsync(new Mediatype
                    {
                        Extension = mediatype.Extension,
                        Description = mediatype.Description,
                        MimeType = mediatype.Mimetype,
                        MimeSubtype = mediatype.Mimesubtype
                    })
                    .ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    this.exceptions.Enqueue(e);
                }
            }
        }
    }
}
