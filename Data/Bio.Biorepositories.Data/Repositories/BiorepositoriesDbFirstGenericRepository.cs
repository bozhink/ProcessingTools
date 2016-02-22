namespace ProcessingTools.Bio.Biorepositories.Data.Repositories
{
    using ProcessingTools.Bio.Biorepositories.Data.Repositories.Contracts;
    using ProcessingTools.Data.Common.Repositories.Factories;

    public class BiorepositoriesDbFirstGenericRepository<T> : EfGenericRepository<BiorepositoriesDbFirstDbContext, T>, IBiorepositoriesDbFirstGenericRepository<T>
        where T : class
    {
        public BiorepositoriesDbFirstGenericRepository(BiorepositoriesDbFirstDbContext context)
            : base(context)
        {
        }
    }
}
