namespace ProcessingTools.Data.Common.Redis.Tests.Fakes.Models.Contracts
{
    using System;
    using ProcessingTools.Contracts;

    public interface ISimpleTimeRecordModel : IEntity
    {
        DateTime LastUpdate { get; set; }

        string Value { get; set; }
    }
}
