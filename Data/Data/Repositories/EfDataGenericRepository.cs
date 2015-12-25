namespace ProcessingTools.Data.Repositories
{
    using Contracts;
    using ProcessingTools.Data.Common.Repositories;

    public class EfDataGenericRepository<T> : EfGenericRepository<IDataDbContext, T>, IDataRepository<T>
        where T : class
    {
        public EfDataGenericRepository(IDataDbContext context)
            : base(context)
        {
        }
    }
}