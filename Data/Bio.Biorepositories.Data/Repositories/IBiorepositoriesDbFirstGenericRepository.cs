namespace ProcessingTools.Bio.Biorepositories.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories;

    public interface IBiorepositoriesDbFirstGenericRepository<T> : IRepository<T>
        where T : class
    {
    }
}