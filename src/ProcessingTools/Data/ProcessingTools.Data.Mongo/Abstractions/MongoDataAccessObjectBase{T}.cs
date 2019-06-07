﻿// <copyright file="MongoDataAccessObjectBase{T}.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Abstractions
{
    using System;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using ProcessingTools.Contracts.DataAccess;

    /// <summary>
    /// MongoDB base for data access object (DAO).
    /// </summary>
    /// <typeparam name="T">Type of database entity.</typeparam>
    public abstract class MongoDataAccessObjectBase<T> : IDataAccessObject
        where T : class
    {
        private readonly IMongoDatabase db;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDataAccessObjectBase{T}"/> class.
        /// </summary>
        /// <param name="databaseProvider">Instance of <see cref="IMongoDatabaseProvider"/>.</param>
        protected MongoDataAccessObjectBase(IMongoDatabaseProvider databaseProvider)
        {
            if (databaseProvider == null)
            {
                throw new ArgumentNullException(nameof(databaseProvider));
            }

            this.db = databaseProvider.Create();
            this.CollectionName = MongoCollectionNameFactory.Create<T>();
        }

        /// <summary>
        /// Gets or sets the collection name.
        /// </summary>
        protected string CollectionName { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="MongoCollectionSettings"/>.
        /// </summary>
        protected MongoCollectionSettings CollectionSettings { get; set; } = null;

        /// <summary>
        /// Gets the collection.
        /// </summary>
        protected IMongoCollection<T> Collection => this.db.GetCollection<T>(this.CollectionName, this.CollectionSettings);

        /// <inheritdoc/>
        public Task<long> SaveChangesAsync() => Task.FromResult(-1L);

        /// <summary>
        /// Gets collection from the same database.
        /// </summary>
        /// <typeparam name="TModel">Type of model of the collection.</typeparam>
        /// <returns><see cref="IMongoCollection{TModel}"/></returns>
        protected IMongoCollection<TModel> GetCollection<TModel>()
        {
            string collectionName = MongoCollectionNameFactory.Create<TModel>();
            return this.db.GetCollection<TModel>(collectionName);
        }
    }
}
