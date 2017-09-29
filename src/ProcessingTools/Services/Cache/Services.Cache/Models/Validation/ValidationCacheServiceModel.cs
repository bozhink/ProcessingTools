namespace ProcessingTools.Services.Cache.Models.Validation
{
    using System;
    using ProcessingTools.Enumerations;
    using ProcessingTools.Models.Contracts.Cache;

    public class ValidationCacheServiceModel : IValidationCacheModel
    {
        public string Content { get; set; }

        public DateTime LastUpdate { get; set; }

        public ValidationStatus Status { get; set; }
    }
}
