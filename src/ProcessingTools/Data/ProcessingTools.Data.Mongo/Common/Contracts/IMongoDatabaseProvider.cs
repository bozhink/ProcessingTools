// <copyright file="IMongoDatabaseProvider.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Common.Contracts
{
    using Data.Contracts;
    using MongoDB.Driver;

    /// <summary>
    /// MongoDB database provider.
    /// </summary>
    public interface IMongoDatabaseProvider : IDatabaseProvider<IMongoDatabase>
    {
    }
}
