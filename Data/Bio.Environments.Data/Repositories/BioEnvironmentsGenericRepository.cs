namespace ProcessingTools.Bio.Environments.Data.Repositories
{
    using Contracts;
    using ProcessingTools.Data.Common.Repositories;

    public class BioEnvironmentsGenericRepository<T> : EfGenericRepository<IBioEnvironmentsDbContext, T>, IBioEnvironmentsRepository<T>
        where T : class
    {
        public BioEnvironmentsGenericRepository(IBioEnvironmentsDbContext context)
            : base(context)
        {
        }
    }
}
