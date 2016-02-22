namespace ProcessingTools.Cache.Data.Models
{
    using System;

    using ProcessingTools.Contracts.Types;

    public class ValidationCacheEntity
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime LastUpdate { get; set; }

        public ValidationStatus Status { get; set; }
    }
}
