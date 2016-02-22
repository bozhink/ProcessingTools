namespace ProcessingTools.Bio.Biorepositories.Data.Repositories
{
    using ProcessingTools.Bio.Biorepositories.Data.Contracts;
    using ProcessingTools.Bio.Biorepositories.Data.Repositories.Contracts;
    using ProcessingTools.Data.Common.Repositories.Factories;

    public class BiorepositoriesRepository<T> : EfGenericRepository<IBiorepositoriesDbContext, T>, IBiorepositoriesRepository<T>
        where T : class
    {
        public BiorepositoriesRepository(IBiorepositoriesDbContext context)
            : base(context)
        {
        }
    }
}
