// <copyright file="MongoRepository{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Common.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using MongoDB.Driver;

    /// <summary>
    /// Generic MongoDB repository.
    /// </summary>
    /// <typeparam name="T">Type of model.</typeparam>
    public abstract class MongoRepository<T> : IMongoRepository<T>
        where T : class
    {
        private readonly IMongoDatabase db;
        private string collectionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoRepository{T}"/> class.
        /// </summary>
        /// <param name="databaseProvider">Database provider.</param>
        protected MongoRepository(IMongoDatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
            {
                throw new ArgumentNullException(nameof(databaseProvider));
            }

            this.db = databaseProvider.Create();
            this.CollectionName = MongoCollectionNameFactory.Create<T>();
        }

        /// <summary>
        /// Gets the collection.
        /// </summary>
        protected IMongoCollection<T> Collection => this.db.GetCollection<T>(this.CollectionName);

        /// <summary>
        /// Gets or sets the collection name.
        /// </summary>
        protected string CollectionName
        {
            get
            {
                return this.collectionName;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                this.collectionName = value;
            }
        }

        /// <inheritdoc/>
        public virtual Task<object> SaveChangesAsync() => Task.FromResult<object>(0);
    }
}
