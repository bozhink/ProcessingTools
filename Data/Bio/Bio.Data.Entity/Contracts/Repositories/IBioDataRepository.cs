namespace ProcessingTools.Bio.Data.Entity.Contracts.Repositories
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IBioDataRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}
