namespace ProcessingTools.Services.Cache.Models
{
    using System;
    using Contracts;
    using ProcessingTools.Contracts.Types;

    public class ValidationCacheServiceModel : IValidationCacheServiceModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime LastUpdate { get; set; }

        public ValidationStatus Status { get; set; }
    }
}
