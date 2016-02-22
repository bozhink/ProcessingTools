namespace ProcessingTools.Services.Cache.Models.Contracts
{
    using System;

    using ProcessingTools.Services.Common.Models.Contracts;
    using ProcessingTools.Services.Common.Types;

    public interface IValidationCacheServiceModel : ISimpleServiceModel
    {
        DateTime LastUpdate { get; set; }

        ValidationStatus Status { get; set; }
    }
}
