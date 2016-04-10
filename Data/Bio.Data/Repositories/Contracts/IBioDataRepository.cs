namespace ProcessingTools.Bio.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IBioDataRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}