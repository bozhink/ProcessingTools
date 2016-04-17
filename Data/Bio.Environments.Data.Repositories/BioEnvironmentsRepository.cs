namespace ProcessingTools.Bio.Environments.Data.Repositories
{
    using ProcessingTools.Bio.Environments.Data.Repositories.Contracts;
    using ProcessingTools.Data.Common.Entity.Repositories;

    public class BioEnvironmentsRepository<T> : EntityGenericRepository<BioEnvironmentsDbContext, T>, IBioEnvironmentsRepository<T>
        where T : class
    {
        public BioEnvironmentsRepository(IBioEnvironmentsDbContextProvider contextProvider)
            : base(contextProvider)
        {
        }
    }
}
