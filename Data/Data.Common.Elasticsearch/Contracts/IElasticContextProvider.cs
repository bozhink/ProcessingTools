namespace ProcessingTools.Data.Common.Elasticsearch.Contracts
{
    using Nest;
    using ProcessingTools.Data.Common.Contracts;

    public interface IElasticContextProvider : IDatabaseProvider<IndexName>
    {
    }
}
