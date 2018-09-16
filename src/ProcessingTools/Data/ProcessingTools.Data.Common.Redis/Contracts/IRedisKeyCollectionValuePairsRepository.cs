// <copyright file="IRedisKeyCollectionValuePairsRepository.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Redis.Contracts
{
    using ProcessingTools.Data.Contracts;

    /// <summary>
    /// Redis key-collection-value pairs repository.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public interface IRedisKeyCollectionValuePairsRepository<T> : IStringKeyCollectionValuePairsRepository<T>
    {
    }
}
