namespace ProcessingTools.Data.Common.Entity.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Repositories.Contracts;

    public interface IEntityGenericRepository<T> : IGenericRepository<T>
        where T : class
    {
    }
}
