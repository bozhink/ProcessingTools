namespace ProcessingTools.Data.Common.Entity.Repositories.Contracts
{
    using ProcessingTools.Contracts.Data.Repositories;

    public interface IEntityRepository<T> : IRepository<T>
        where T : class
    {
    }
}
