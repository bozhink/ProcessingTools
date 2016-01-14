namespace ProcessingTools.Bio.Biorepositories.Data.Repositories
{
    using Contracts;
    using ProcessingTools.Data.Common.Repositories;

    public class EfBiorepositoriesGenericRepository<T> : EfGenericRepository<IBiorepositoriesDbContext, T>, IBiorepositoriesRepository<T>
        where T : class
    {
        public EfBiorepositoriesGenericRepository(IBiorepositoriesDbContext context)
            : base(context)
        {
        }
    }
}
