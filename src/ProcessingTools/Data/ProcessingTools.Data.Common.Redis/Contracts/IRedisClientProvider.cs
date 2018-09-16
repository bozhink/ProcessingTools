// <copyright file="IRedisClientProvider.cs" company="ProcessingTools">
// Copyright (c) 2018 ProcessingTools. All rights reserved.
// </copyright>

namespace ProcessingTools.Data.Common.Redis.Contracts
{
    using ProcessingTools.Data.Contracts;
    using ServiceStack.Redis;

    /// <summary>
    /// Redis client provider.
    /// </summary>
    public interface IRedisClientProvider : IDatabaseProvider<IRedisClient>
    {
    }
}
