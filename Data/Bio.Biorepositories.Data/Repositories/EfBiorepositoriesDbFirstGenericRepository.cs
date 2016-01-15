namespace ProcessingTools.Bio.Biorepositories.Data.Repositories
{
    using ProcessingTools.Data.Common.Repositories;

    public class EfBiorepositoriesDbFirstGenericRepository<T> : EfGenericRepository<BiorepositoriesDbFirstDbContext, T>, IBiorepositoriesDbFirstGenericRepository<T>
        where T : class
    {
        public EfBiorepositoriesDbFirstGenericRepository(BiorepositoriesDbFirstDbContext context)
            : base(context)
        {
        }
    }
}
