// <copyright file="IMongoDatabaseProvider.cs" company="ProcessingTools">
// Copyright (c) 2021 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo
{
    using MongoDB.Driver;
    using ProcessingTools.Data.Contracts;

    /// <summary>
    /// MongoDB database provider.
    /// </summary>
    public interface IMongoDatabaseProvider : IDatabaseProvider<IMongoDatabase>
    {
        /// <summary>
        /// Gets collection from specified data base.
        /// </summary>
        /// <typeparam name="T">Type of the data model.</typeparam>
        /// <param name="db">Data base.</param>
        /// <returns>Instance of <see cref="IMongoCollection{T}"/>.</returns>
        IMongoCollection<T> GetCollection<T>(IMongoDatabase db);

        /// <summary>
        /// Gets collection from specified data base with custom settings.
        /// </summary>
        /// <typeparam name="T">Type of the data model.</typeparam>
        /// <param name="db">Data base.</param>
        /// <param name="settings">Settings for the collection.</param>
        /// <returns>Instance of <see cref="IMongoCollection{T}"/>.</returns>
        IMongoCollection<T> GetCollection<T>(IMongoDatabase db, MongoCollectionSettings settings);
    }
}
