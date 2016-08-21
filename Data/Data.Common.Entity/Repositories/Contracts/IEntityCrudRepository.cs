namespace ProcessingTools.Data.Common.Entity.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IEntityCrudRepository<T> : ICrudRepository<T>, IEntityRepository<T>
        where T : class
    {
    }
}
