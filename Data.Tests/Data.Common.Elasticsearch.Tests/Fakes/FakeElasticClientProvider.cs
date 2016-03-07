namespace ProcessingTools.Data.Common.Elasticsearch.Tests.Fakes
{
    using System;
    using Nest;
    using ProcessingTools.Data.Common.Elasticsearch.Contracts;

    public class FakeElasticClientProvider : IElasticClientProvider
    {
        public IElasticClient Create()
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);
            return client;
        }
    }
}