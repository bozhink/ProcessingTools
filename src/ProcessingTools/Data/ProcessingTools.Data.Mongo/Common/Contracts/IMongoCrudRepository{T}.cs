// <copyright file="IMongoCrudRepository{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Common.Contracts
{
    using Data.Contracts;

    /// <summary>
    /// Generic MongoDB CRUD repository.
    /// </summary>
    /// <typeparam name="T">Type of model.</typeparam>
    public interface IMongoCrudRepository<T> : ICrudRepository<T>, IMongoRepository<T>
        where T : class
    {
    }
}
