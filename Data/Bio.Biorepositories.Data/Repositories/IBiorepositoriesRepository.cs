namespace ProcessingTools.Bio.Biorepositories.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories;

    public interface IBiorepositoriesRepository<T> : IEfRepository<T>
        where T : class
    {
    }
}