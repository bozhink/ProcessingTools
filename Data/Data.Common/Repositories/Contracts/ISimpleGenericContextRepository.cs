namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using Models.Contracts;
    using ProcessingTools.Contracts.Data.Repositories;

    public interface ISimpleGenericContextRepository<TEntity> : IGenericContextRepository<string, int, TEntity>, IRepository<TEntity>
        where TEntity : IEntity
    {
    }
}
