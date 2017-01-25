namespace ProcessingTools.Services.Cache.Models.Validation
{
    using System;
    using Contracts.Models.Validation;
    using ProcessingTools.Enumerations;

    public class ValidationCacheServiceModel : IValidationCacheServiceModel
    {
        public string Content { get; set; }

        public DateTime LastUpdate { get; set; }

        public ValidationStatus Status { get; set; }
    }
}
