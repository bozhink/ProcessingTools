namespace ProcessingTools.Bio.Data.Repositories
{
    using ProcessingTools.Bio.Data.Contracts;
    using ProcessingTools.Bio.Data.Repositories.Contracts;
    using ProcessingTools.Data.Common.Repositories;

    public class BioDataRepository<T> : EfGenericRepository<IBioDbContext, T>, IBioDataRepository<T>
        where T : class
    {
        public BioDataRepository(IBioDbContext context)
            : base(context)
        {
        }
    }
}
