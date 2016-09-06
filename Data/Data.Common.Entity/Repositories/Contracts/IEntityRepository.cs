namespace ProcessingTools.Data.Common.Entity.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IEntityRepository<T> : IRepository<T>
        where T : class
    {
    }
}
