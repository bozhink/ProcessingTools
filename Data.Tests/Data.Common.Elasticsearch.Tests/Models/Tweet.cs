namespace ProcessingTools.Data.Common.Elasticsearch.Tests.Models
{
    using System;
    using ProcessingTools.Data.Common.Models.Contracts;

    public class Tweet : IEntity
    {
        public int Id { get; set; }

        public string User { get; set; }

        public DateTime PostDate { get; set; }

        public string Message { get; set; }
    }
}
