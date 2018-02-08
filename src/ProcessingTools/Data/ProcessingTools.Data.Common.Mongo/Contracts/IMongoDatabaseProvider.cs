// <copyright file="IMongoDatabaseProvider.cs" company="ProcessingTools">
// Copyright (c) 2017 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Mongo.Contracts
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
