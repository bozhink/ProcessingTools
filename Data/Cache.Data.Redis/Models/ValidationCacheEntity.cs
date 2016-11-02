namespace ProcessingTools.Cache.Data.Redis.Models
{
    using System;
    using ProcessingTools.Cache.Data.Common.Contracts.Models;
    using ProcessingTools.Enumerations;

    public class ValidationCacheEntity : IValidationCacheEntity
    {
        public string Content { get; set; }

        public DateTime LastUpdate { get; set; }

        public ValidationStatus Status { get; set; }
    }
}
