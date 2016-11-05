namespace ProcessingTools.Bio.Environments.Data.Entity.Repositories
{
    using ProcessingTools.Bio.Environments.Data.Entity.Contracts;
    using ProcessingTools.Bio.Environments.Data.Entity.Contracts.Repositories;
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
