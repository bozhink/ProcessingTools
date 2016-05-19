namespace ProcessingTools.Data.Common.Elasticsearch.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IElasticsearchGenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
    }
}
