namespace ProcessingTools.Services.Cache.Models
{
    using System;
    using ProcessingTools.Contracts;
    using ProcessingTools.Enumerations;

    public class ValidationCacheServiceModel : IEntity
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime LastUpdate { get; set; }

        public ValidationStatus Status { get; set; }
    }
}
