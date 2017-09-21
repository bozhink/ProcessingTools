namespace ProcessingTools.Contracts.Data.Cache.Models
{
    using System;
    using ProcessingTools.Enumerations;

    public interface IValidationCacheEntity
    {
        string Content { get; }

        DateTime LastUpdate { get; }

        ValidationStatus Status { get; }
    }
}
