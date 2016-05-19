namespace ProcessingTools.Data.Common.Redis.Tests.Fakes.Models
{
    using System;

    using Contracts;

    public class SimpleTimeRecordModel : ISimpleTimeRecordModel
    {
        public int Id { get; set; }

        public DateTime LastUpdate { get; set; }

        public string Value { get; set; }
    }
}