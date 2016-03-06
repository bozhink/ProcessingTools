namespace ProcessingTools.Bio.Environments.Data.Repositories
{
    using ProcessingTools.Bio.Environments.Data.Contracts;
    using ProcessingTools.Bio.Environments.Data.Repositories.Contracts;
    using ProcessingTools.Data.Common.Entity.Repositories;

    public class BioEnvironmentsRepository<T> : EfGenericRepository<BioEnvironmentsDbContext, T>, IBioEnvironmentsRepository<T>
        where T : class
    {
        public BioEnvironmentsRepository(IBioEnvironmentsDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}
