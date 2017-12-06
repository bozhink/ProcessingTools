namespace ProcessingTools.Cache.Data.Redis.Models
{
    using System;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Contracts.Models.Cache;

    public class ValidationCacheEntity : IValidationCacheModel
    {
        public string Content { get; set; }

        public DateTime LastUpdate { get; set; }

        public ValidationStatus Status { get; set; }
    }
}
