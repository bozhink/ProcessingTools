// <copyright file="MediatypesMongoDatabaseSeeder.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Seed.Files
{
    using System;
    using System.Collections.Concurrent;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using Newtonsoft.Json;
    using ProcessingTools.Common.Constants.Configuration;
    using ProcessingTools.Data.Models.Mongo.Files;
    using ProcessingTools.Data.Mongo;
    using ProcessingTools.Data.Seed.Files.Models;

    /// <summary>
    /// Mediatypes MongoDB database seeder.
    /// </summary>
    public class MediatypesMongoDatabaseSeeder : IMediatypesMongoDatabaseSeeder
    {
        private readonly IMongoDatabase db;

        private ConcurrentQueue<Exception> exceptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediatypesMongoDatabaseSeeder"/> class.
        /// </summary>
        /// <param name="provider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        public MediatypesMongoDatabaseSeeder(IMongoDatabaseProvider provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            this.db = provider.Create();
            this.exceptions = new ConcurrentQueue<Exception>();
        }

        /// <inheritdoc/>
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
                Extension = m.Extension.ToLower(CultureInfo.CurrentCulture),
                Mimetype = m.Mimetype.ToLower(CultureInfo.CurrentCulture),
                Mimesubtype = m.Mimesubtype.ToLower(CultureInfo.CurrentCulture),
                Description = m.Description,
            })
            .ToArray();
        }

        private async Task ImportMediatypesToDatabase(ExtensionJson[] mediatypes)
        {
            var collection = this.db.GetCollection<Mediatype>(MongoCollectionNameFactory.Create<Mediatype>());

            foreach (var mediatype in mediatypes)
            {
                try
                {
                    await collection.InsertOneAsync(new Mediatype
                    {
                        Extension = mediatype.Extension,
                        Description = mediatype.Description,
                        MimeType = mediatype.Mimetype,
                        MimeSubtype = mediatype.Mimesubtype,
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
