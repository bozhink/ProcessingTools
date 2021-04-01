// <copyright file="MongoBiorepositoriesDatabaseInitializer.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Bio.Biorepositories
{
    using System;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Contracts.Data;
    using ProcessingTools.Data.Models.Mongo.Bio.Biorepositories;

    /// <summary>
    /// MongoDB implementation of <see cref="IBiorepositoriesDatabaseInitializer"/>.
    /// </summary>
    public class MongoBiorepositoriesDatabaseInitializer : IBiorepositoriesDatabaseInitializer
    {
        private readonly IMongoDatabase db;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoBiorepositoriesDatabaseInitializer"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        public MongoBiorepositoriesDatabaseInitializer(IMongoDatabaseProvider databaseProvider)
        {
            if (databaseProvider is null)
            {
                throw new ArgumentNullException(nameof(databaseProvider));
            }

            this.db = databaseProvider.Create();
        }

        /// <inheritdoc/>
        public async Task<object> InitializeAsync()
        {
            await this.db.GetCollection<Collection>(MongoCollectionNameFactory.Create<Collection>()).Indexes
                .CreateManyAsync(new[]
                {
                    new CreateIndexModel<Collection>(Builders<Collection>.IndexKeys.Ascending(c => c.CollectionName), new CreateIndexOptions { Unique = false }),
                })
                .ConfigureAwait(false);

            return true;
        }
    }
}
