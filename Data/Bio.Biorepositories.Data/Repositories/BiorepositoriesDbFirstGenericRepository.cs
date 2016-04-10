namespace ProcessingTools.Bio.Biorepositories.Data.Repositories
{
    using ProcessingTools.Bio.Biorepositories.Data.Repositories.Contracts;
    using ProcessingTools.Data.Common.Entity.Repositories;

    public class BiorepositoriesDbFirstGenericRepository<T> : EntityGenericRepository<BiorepositoriesDbFirstDbContext, T>, IBiorepositoriesDbFirstGenericRepository<T>
        where T : class
    {
        public BiorepositoriesDbFirstGenericRepository(BiorepositoriesDbFirstDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}
