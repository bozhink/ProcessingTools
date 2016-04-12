namespace ProcessingTools.Bio.Biorepositories.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IBiorepositoriesRepository<T> : IEntityGenericRepository<T>
        where T : class
    {
    }
}