namespace ProcessingTools.Cache.Data.Models
{
    using System;

    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Data.Common.Models.Contracts;

    public class ValidationCacheEntity : IEntity
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime LastUpdate { get; set; }

        public ValidationStatus Status { get; set; }
    }
}
