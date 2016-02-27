namespace ProcessingTools.Data.Common.Redis.Tests.Fakes.Models.Contracts
{
    using System;

    using ProcessingTools.Data.Common.Models.Contracts;

    public interface ISimpleTimeRecordModel : IEntity
    {
        DateTime LastUpdate { get; set; }

        string Value { get; set; }
    }
}