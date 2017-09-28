namespace ProcessingTools.Data.Common.Entity.Repositories.Contracts
{
    using ProcessingTools.Data.Contracts.Repositories;

    public interface IEntityRepository<T> : IRepository<T>
        where T : class
    {
    }
}
