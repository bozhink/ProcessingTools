namespace ProcessingTools.Bio.Biorepositories.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories;

    public interface IBiorepositoriesRepository<T> : IRepository<T>
        where T : class
    {
    }
}