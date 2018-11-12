// <copyright file="IMongoRepository{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Abstractions
{
    using Contracts;

    /// <summary>
    /// Generic MongoDB repository.
    /// </summary>
    /// <typeparam name="T">Type of model.</typeparam>
    public interface IMongoRepository<T> : IRepository<T>
        where T : class
    {
    }
}
