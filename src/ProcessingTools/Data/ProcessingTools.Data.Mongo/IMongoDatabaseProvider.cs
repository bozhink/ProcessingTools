// <copyright file="IMongoDatabaseProvider.cs" company="ProcessingTools">
// Copyright (c) 2019 ProcessingTools. All rights reserved.
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
    }
}
