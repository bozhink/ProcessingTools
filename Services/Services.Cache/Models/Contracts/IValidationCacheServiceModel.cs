namespace ProcessingTools.Services.Cache.Models.Contracts
{
    using System;

    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Services.Common.Models.Contracts;

    public interface IValidationCacheServiceModel : ISimpleServiceModel
    {
        DateTime LastUpdate { get; set; }

        ValidationStatus Status { get; set; }
    }
}
