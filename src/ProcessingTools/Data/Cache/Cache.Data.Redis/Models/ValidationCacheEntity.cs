namespace ProcessingTools.Cache.Data.Redis.Models
{
    using System;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts.Cache;

    public class ValidationCacheEntity : IValidationCacheEntity
    {
        public string Content { get; set; }

        public DateTime LastUpdate { get; set; }

        public ValidationStatus Status { get; set; }
    }
}
