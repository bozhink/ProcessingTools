namespace ProcessingTools.Cache.Data.Models
{
    using System;

    using Contracts;
    using ProcessingTools.Contracts.Types;

    public class ValidationCacheEntity : IValidationCacheEntity
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime LastUpdate { get; set; }

        public ValidationStatus Status { get; set; }
    }
}
