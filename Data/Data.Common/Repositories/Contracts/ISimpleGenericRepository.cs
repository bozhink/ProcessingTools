namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using Models.Contracts;

    public interface ISimpleGenericRepository<TEntity> : IGenericContextRepository<string, int, TEntity>
        where TEntity : IEntity
    {
    }
}
