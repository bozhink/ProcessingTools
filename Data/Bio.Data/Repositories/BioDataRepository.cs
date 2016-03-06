namespace ProcessingTools.Bio.Data.Repositories
{
    using ProcessingTools.Bio.Data.Contracts;
    using ProcessingTools.Bio.Data.Repositories.Contracts;
    using ProcessingTools.Data.Common.Entity.Repositories;

    public class BioDataRepository<T> : EfGenericRepository<BioDbContext, T>, IBioDataRepository<T>
        where T : class
    {
        public BioDataRepository(IBioDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}