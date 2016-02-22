namespace ProcessingTools.Cache.Data.Models.Contracts
{
    using System;

    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Data.Common.Redis.Models.Contracts;

    public interface IValidationCacheEntity : IRedisEntity
    {
        string Content { get; set; }

        DateTime LastUpdate { get; set; }

        ValidationStatus Status { get; set; }
    }
}
