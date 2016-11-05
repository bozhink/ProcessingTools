namespace ProcessingTools.Bio.Data.Entity.Repositories
{
    using Contracts;
    using Contracts.Repositories;
    using ProcessingTools.Data.Common.Entity.Repositories;

    public class BioDataRepository<T> : EntityGenericRepository<BioDbContext, T>, IBioDataRepository<T>
        where T : class
    {
        public BioDataRepository(IBioDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}
