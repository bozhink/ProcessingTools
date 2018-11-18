namespace ProcessingTools.Data.Entity.Bio
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class BioDataRepository<T> : EntityGenericRepository<BioDbContext, T>, IBioDataRepository<T>
        where T : class
    {
        public BioDataRepository(IDbContextProvider<BioDbContext> contextProvider)
            : base(contextProvider)
        {
        }
    }
}
