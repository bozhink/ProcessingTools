// <copyright file="IMongoGenericRepository{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Mongo.Contracts
{
    /// <summary>
    /// Generic MongoDB repository.
    /// </summary>
    /// <typeparam name="T">Type of model.</typeparam>
    public interface IMongoGenericRepository<T> : IMongoSearchableRepository<T>, IMongoCrudRepository<T>
        where T : class
    {
    }
}
