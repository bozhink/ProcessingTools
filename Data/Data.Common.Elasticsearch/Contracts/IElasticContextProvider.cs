namespace ProcessingTools.Data.Common.Elasticsearch.Contracts
{
    using Nest;

    public interface IElasticContextProvider
    {
        IndexName Context { get; }
    }
}
