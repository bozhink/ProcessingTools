namespace ProcessingTools.Services.Cache.Models
{
    using System;
    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Services.Common.Models.Contracts;

    public class ValidationCacheServiceModel : ISimpleServiceModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime LastUpdate { get; set; }

        public ValidationStatus Status { get; set; }
    }
}
