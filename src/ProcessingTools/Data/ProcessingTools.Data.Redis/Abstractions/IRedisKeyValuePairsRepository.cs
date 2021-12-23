// <copyright file="IRedisKeyValuePairsRepository.cs" company="ProcessingTools">
// Copyright (c) 2022 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Redis.Abstractions
{
    using ProcessingTools.Data.Contracts;

    /// <summary>
    /// Redis key-value pairs repository.
    /// </summary>
    /// <typeparam name="T">Type of the entity.</typeparam>
    public interface IRedisKeyValuePairsRepository<T> : IStringKeyValuePairsRepository<T>
    {
    }
}
