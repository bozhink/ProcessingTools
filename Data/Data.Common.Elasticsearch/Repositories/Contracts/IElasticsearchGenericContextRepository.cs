namespace ProcessingTools.Data.Common.Elasticsearch.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Models.Contracts;
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IElasticsearchGenericContextRepository<TEntity> : ISimpleGenericContextRepository<TEntity>
        where TEntity : IEntity
    {
    }
}
