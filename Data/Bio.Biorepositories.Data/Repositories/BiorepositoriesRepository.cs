namespace ProcessingTools.Bio.Biorepositories.Data.Repositories
{
    using ProcessingTools.Bio.Biorepositories.Data.Contracts;
    using ProcessingTools.Bio.Biorepositories.Data.Repositories.Contracts;
    using ProcessingTools.Data.Common.Entity.Repositories;

    public class BiorepositoriesRepository<T> : EntityGenericRepository<BiorepositoriesDbContext, T>, IBiorepositoriesRepository<T>
        where T : class
    {
        public BiorepositoriesRepository(IBiorepositoriesDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}
