namespace ProcessingTools.Data.Common.Entity.Repositories.Contracts
{
    using ProcessingTools.Data.Contracts.Repositories;

    public interface IEntityCrudRepository<T> : ICrudRepository<T>, IEntityRepository<T>
        where T : class
    {
    }
}
