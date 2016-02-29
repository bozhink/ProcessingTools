namespace ProcessingTools.Data.Common.Elasticsearch.Contracts
{
    using Nest;

    public interface IElasticClientProvider
    {
        IElasticClient Create();
    }
}
