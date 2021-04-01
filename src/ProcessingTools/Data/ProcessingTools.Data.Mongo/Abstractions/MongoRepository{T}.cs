// <copyright file="MongoRepository{T}.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Abstractions
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using MongoDB.Driver;

    /// <summary>
    /// Generic MongoDB repository.
    /// </summary>
    /// <typeparam name="T">Type of database model.</typeparam>
    public class MongoRepository<T> : IMongoCrudRepository<T>
        where T : class
    {
        private readonly IMongoCollection<T> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoRepository{T}"/> class.
        /// </summary>
        /// <param name="databaseProvider">Database provider.</param>
        public MongoRepository(IMongoDatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
            {
                throw new ArgumentNullException(nameof(databaseProvider));
            }

            string collectionName = MongoCollectionNameFactory.Create<T>();
            IMongoDatabase db = databaseProvider.Create();

            this.collection = db.GetCollection<T>(collectionName);
        }

        /// <inheritdoc/>
        public virtual IQueryable<T> Query => this.collection.AsQueryable();

        /// <inheritdoc/>
        public virtual Task<object> AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return this.AddInternalAsync(entity);
        }

        /// <inheritdoc/>
        public virtual Task<object> SaveChangesAsync() => Task.FromResult<object>(0);

        private async Task<object> AddInternalAsync(T entity)
        {
            await this.collection.InsertOneAsync(entity).ConfigureAwait(false);

            return entity;
        }
    }
}
