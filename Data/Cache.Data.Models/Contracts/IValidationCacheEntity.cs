﻿namespace ProcessingTools.Cache.Data.Models.Contracts
{
    using System;

    using ProcessingTools.Contracts.Types;
    using ProcessingTools.Data.Common.Models.Contracts;

    public interface IValidationCacheEntity : IEntity
    {
        string Content { get; set; }

        DateTime LastUpdate { get; set; }

        ValidationStatus Status { get; set; }
    }
}
