namespace ProcessingTools.Bio.Data.Repositories
{
    using Contracts;
    using ProcessingTools.Data.Common.Repositories;

    public class EfBioDataGenericRepository<T> : EfGenericRepository<IBioDbContext, T>, IBioDataRepository<T>
        where T : class
    {
        public EfBioDataGenericRepository(IBioDbContext context)
            : base(context)
        {
        }
    }
}
