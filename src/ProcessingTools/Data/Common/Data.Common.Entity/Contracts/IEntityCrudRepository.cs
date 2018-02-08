namespace ProcessingTools.Data.Common.Entity.Contracts
{
    using ProcessingTools.Data.Contracts;

    public interface IEntityCrudRepository<T> : ICrudRepository<T>, IEntityRepository<T>
        where T : class
    {
    }
}
