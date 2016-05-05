namespace ProcessingTools.Data.Common.Elasticsearch.Tests.Fakes
{
    using Models;
    using ProcessingTools.Data.Common.Elasticsearch.Repositories;

    public class FakeElasticsearchRepository : ElasticsearchGenericRepository<Tweet>
    {
        public FakeElasticsearchRepository()
            : base(new FakeElasticContextProvider(), new FakeElasticClientProvider())
        {
        }
    }
}
