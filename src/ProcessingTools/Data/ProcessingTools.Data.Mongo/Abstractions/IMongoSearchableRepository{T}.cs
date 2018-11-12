// <copyright file="IMongoSearchableRepository{T}.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Mongo.Abstractions
{
    using ProcessingTools.Data.Contracts;

    /// <summary>
    /// Generic MongoDB searchable repository.
    /// </summary>
    /// <typeparam name="T">Type of model.</typeparam>
    public interface IMongoSearchableRepository<T> : ISearchableRepository<T>, IMongoRepository<T>
        where T : class
    {
    }
}
