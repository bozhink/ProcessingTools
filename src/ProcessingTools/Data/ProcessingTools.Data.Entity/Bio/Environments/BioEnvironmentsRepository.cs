namespace ProcessingTools.Data.Entity.Bio.Environments
{
    using ProcessingTools.Data.Entity.Abstractions;

    public class BioEnvironmentsRepository<T> : EntityGenericRepository<BioEnvironmentsDbContext, T>, IBioEnvironmentsRepository<T>
        where T : class
    {
        public BioEnvironmentsRepository(IDbContextProvider<BioEnvironmentsDbContext> contextProvider)
            : base(contextProvider)
        {
        }
    }
}
