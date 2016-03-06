namespace ProcessingTools.Bio.Biorepositories.Data.Repositories.Contracts
{
    using ProcessingTools.Data.Common.Entity.Repositories.Contracts;

    public interface IBiorepositoriesRepository<T> : IEfRepository<T>
        where T : class
    {
    }
}