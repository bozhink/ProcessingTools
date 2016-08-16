namespace ProcessingTools.Data.Common.Repositories.Contracts
{
    public interface ICountableIterableRepository<TEntity> : ICountableRepository<TEntity>, IIterableRepository<TEntity>
    {
    }
}
