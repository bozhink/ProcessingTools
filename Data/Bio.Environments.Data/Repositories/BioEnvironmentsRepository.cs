namespace ProcessingTools.Bio.Environments.Data.Repositories
{
    using ProcessingTools.Bio.Environments.Data.Contracts;
    using ProcessingTools.Bio.Environments.Data.Repositories.Contracts;
    using ProcessingTools.Data.Common.Repositories.Factories;

    public class BioEnvironmentsRepository<T> : EfGenericRepository<IBioEnvironmentsDbContext, T>, IBioEnvironmentsRepository<T>
        where T : class
    {
        public BioEnvironmentsRepository(IBioEnvironmentsDbContext context)
            : base(context)
        {
        }
    }
}
