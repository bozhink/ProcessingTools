namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    using Models.Contracts;

    public interface ISimpleGenericRepository<TEntity> : IGenericRepository<string, int, TEntity>
        where TEntity : IEntity
    {
    }
}
